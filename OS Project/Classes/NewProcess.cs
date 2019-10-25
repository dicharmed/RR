using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_program
{
    public class NewProcess
    {
        public string name { get; set; }
        public int ID { get; set; }
        public int time { get; set; }
        public int switchTime { get; set; }
        public string status { get; set; }
        public int quantum { get; set; }
        public int remainingTime { get; set; }

        public NewProcess() //constructor
        {
            name = "";
            ID = 0;
            time = 0;
            switchTime = 0; //время переключения
            status = "Готов";
            remainingTime = 0;
        }

    }
}