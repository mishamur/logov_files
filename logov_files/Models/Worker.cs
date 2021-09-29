using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace logov_files.Models
{

   [Serializable]
    public class Worker
    {
        private readonly int overtime = 144;
        public string Fio { get; set; }
        public string TabNumber { get; set; }
        public int CountHour { get; set; }
        public int Rate { get; set; }

        private double salary;
        public double Salary { 
            get
            { 
                if(this.CountHour > overtime)
                {
                    double remains = CountHour - overtime;
                    salary = overtime * Rate + remains * Rate * 2;
                    return salary;
                }

                salary = CountHour * Rate;
                return salary;
               
            }
            set 
            {
                salary = value;
            } 
        }
        public Worker(string Fio, string TabNumber, int CountHour, int Rate)
        {
            this.Fio = Fio;
            this.TabNumber = TabNumber;
            this.CountHour = CountHour;
            this.Rate = Rate;

        }
        public override string ToString()
        {
            return $"{Fio}  зп: {Salary}";
        }
    }
}
