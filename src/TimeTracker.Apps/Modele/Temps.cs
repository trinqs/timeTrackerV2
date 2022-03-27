using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Apps.Modele
{
    internal class Temps
    {
        private int id { get; set; }
        private DateTime start_time { get; set; }
        private DateTime end_time { get; set; }

        public Temps(int _id, DateTime _start_time, DateTime _end_time)
        {
            id = _id;
            start_time = _start_time;
            end_time = _end_time;
        }

        public Temps()
        {
            start_time = DateTime.Now;
        }

        public void Fin()
        {
            end_time = DateTime.Now;
        }
    }
}
