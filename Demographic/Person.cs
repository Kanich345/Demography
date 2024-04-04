using Demographic.FileOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demographic
{
    public class Person
    {
        public Gender Gender{ get; }
        public int Year { get; }
        public bool IsAlive { get; set; }
        public int YearDeath { get; set; }

        private int age;

        public int Age => age;

        public event Action<Person> ChildBirth, PersonDeath;
        private const double probabilityBirth = 0.151;
        private const double probabilityBirthGirl = 0.55;
        private static List<DeathData> deathDatas = FileManager.Instance.ReadDeath();

        public Person(Gender gender, int year)
        {
            Gender = gender;
            Year = year;
            IsAlive = true;
        }

        public void NewYear(int year)
        {
            if (!IsAlive)
                return;

            age = year - Year;
            
            DeathData deathData = deathDatas.FirstOrDefault(t => age >= t.StartAge && age <= t.FinishAge);
            double probDeath = 1;
            if (deathData != null)
                probDeath = Gender == Gender.Male ? deathData.ProbDeathMale : deathData.ProbDeathFemale;

            if (ProbabilityCalculator.IsEventHappened(probDeath))
            {
                IsAlive = false;
                PersonDeath?.Invoke(this);
            }

            if (IsAlive && Gender == Gender.Female && age >= 18 && age <= 45)
            {
                if (ProbabilityCalculator.IsEventHappened(probabilityBirth))
                {
                    Gender gender = ProbabilityCalculator.IsEventHappened(probabilityBirthGirl) ? Gender.Female : Gender.Male;
                    Person p = new Person(gender, year);
                    ChildBirth?.Invoke(p);
                }
            }
        }
    }
}
