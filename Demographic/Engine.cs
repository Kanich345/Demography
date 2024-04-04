using Demographic.FileOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demographic
{
    public class Engine : IEngine
    {
        private List<Person> people = new List<Person>();
        public event Action<int> YearTick;
        private int startYear;
        private int finishYear;
        private const int group = 1000;
        private List<(int, int)> statisticPopulation = new List<(int, int)>();
        private List<(int, int)> statisticMalePopulation = new List<(int, int)>();
        private List<(int, int)> statisticFemalePopulation = new List<(int, int)>();
        private List<int> statisticByAgeMale = new List<int>();
        private List<int> statisticByAgeFemale = new List<int>();

        public List<(int, int)> StatisticPopulation => statisticPopulation;
        public List<(int, int)> StatisticMalePopulation => statisticMalePopulation;
        public List<(int, int)> StatisticFemalePopulation => statisticFemalePopulation;
        public List<int> StatisticByAgeMale => statisticByAgeMale;
        public List<int> StatisticByAgeFemale => statisticByAgeFemale;

        public Engine(int population, int startYear, int finishYear)
        {
            this.startYear = startYear;
            this.finishYear = finishYear;

            CreatePeople(population * group, startYear);
        }

        private void CreatePeople(int population, int startYear)
        {
            //population /= 1000;

            List<(int, double)> list = FileManager.Instance.ReadInitialAge();
            foreach (var item in list)
            {
                int count = (int)(population * Math.Round(item.Item2) / 1000);
                for (int i = 0; i < count && people.Count < population; i++)
                {
                    Gender gender = i % 2 == 0 ? Gender.Male : Gender.Female;
                    Person person = new Person(gender, startYear - item.Item1);
                    YearTick += person.NewYear;
                    person.ChildBirth += Person_ChildBirth;
                    person.PersonDeath += Person_PersonDeath;
                    people.Add(person);
                }
            }
        }

        private void Person_PersonDeath(Person obj)
        {
            //people.Remove(obj);
            YearTick -= obj.NewYear;
        }

        private void Person_ChildBirth(Person obj)
        {
            people.Add(obj);
            YearTick += obj.NewYear;
            obj.ChildBirth += Person_ChildBirth;
            obj.PersonDeath += Person_PersonDeath;
        }

        public void Start()
        {
            for (int year = startYear; year < finishYear; year++)
            {
                YearTick?.Invoke(year);
                statisticPopulation.Add((year, people.Count(t=>t.IsAlive)));

                int countMale = people.Count(t => t.Gender == Gender.Male && t.IsAlive);
                int countFemale = people.Count(t => t.Gender == Gender.Female && t.IsAlive);

                statisticMalePopulation.Add((year, countMale));
                statisticFemalePopulation.Add((year, countFemale));
            }

            int countMale_0_18 = people.Count(t => t.Gender == Gender.Male && t.IsAlive && t.Age >= 0 && t.Age <= 18);
            int countMale_19_44 = people.Count(t => t.Gender == Gender.Male && t.IsAlive && t.Age >= 19 && t.Age <= 44);
            int countMale_45_65 = people.Count(t => t.Gender == Gender.Male && t.IsAlive && t.Age >= 45 && t.Age <= 65);
            int countMale_66_100 = people.Count(t => t.Gender == Gender.Male && t.IsAlive && t.Age >= 66 && t.Age <= 100);

            statisticByAgeMale.Add(countMale_0_18);
            statisticByAgeMale.Add(countMale_19_44);
            statisticByAgeMale.Add(countMale_45_65);
            statisticByAgeMale.Add(countMale_66_100);

            int countFemale_0_18 = people.Count(t => t.Gender == Gender.Female && t.IsAlive && t.Age >= 0 && t.Age <= 18);
            int countFemale_19_44 = people.Count(t => t.Gender == Gender.Female && t.IsAlive && t.Age >= 19 && t.Age <= 44);
            int countFemale_45_65 = people.Count(t => t.Gender == Gender.Female && t.IsAlive && t.Age >= 45 && t.Age <= 65);
            int countFemale_66_100 = people.Count(t => t.Gender == Gender.Female && t.IsAlive && t.Age >= 66 && t.Age <= 100);

            statisticByAgeFemale.Add(countFemale_0_18);
            statisticByAgeFemale.Add(countFemale_19_44);
            statisticByAgeFemale.Add(countFemale_45_65);
            statisticByAgeFemale.Add(countFemale_66_100);
        }
    }
}
