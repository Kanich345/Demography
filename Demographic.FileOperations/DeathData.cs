using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demographic.FileOperations
{
    public class DeathData
    {
        public DeathData(int startAge, int finishAge, double probDeathMale, double probDeathFemale)
        {
            StartAge = startAge;
            FinishAge = finishAge;
            ProbDeathMale = probDeathMale;
            ProbDeathFemale = probDeathFemale;
        }

        public int StartAge { get; }
        public int FinishAge { get; }
        public double ProbDeathMale { get; }
        public double ProbDeathFemale { get; }
    }
}
