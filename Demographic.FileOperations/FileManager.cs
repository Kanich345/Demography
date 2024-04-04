using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demographic.FileOperations
{
    public class FileManager
    {
        private static FileManager instance;
        private string filenameInitialAge;
        private string filenameDeath;

        public static FileManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new FileManager();
                return instance;
            }
        }

        private FileManager() { }

        public void SetFilenameInitialAge(string filename)
        {
            filenameInitialAge = filename;
        }

        public void SetFilenameDeath(string filename)
        {
            filenameDeath = filename;
        }

        public List<(int, double)> ReadInitialAge()
        {
            List<(int, double)> list = new List<(int, double)>();

            using (StreamReader reader = new StreamReader(filenameInitialAge))
            {
                string header = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] a = line.Split(',');

                    int age = int.Parse(a[0]);
                    double p = double.Parse(a[1].Replace('.', ','));

                    list.Add((age, p));
                }
            }
            return list;
        }

        public List<DeathData> ReadDeath()
        {
            List<DeathData> list = new List<DeathData>();

            using (StreamReader reader = new StreamReader(filenameDeath))
            {
                string header = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine();
                    string[] a = s.Split(',');
                    int startAge = int.Parse(a[0]);
                    int finishAge = int.Parse(a[1]);
                    double maleProb = double.Parse(a[2].Replace('.', ','));
                    double femaleProb = double.Parse(a[3].Replace('.', ','));

                    DeathData deathData = new DeathData(startAge, finishAge, maleProb, femaleProb);
                    list.Add(deathData);
                }
            }

            return list;
        }
    }
}
