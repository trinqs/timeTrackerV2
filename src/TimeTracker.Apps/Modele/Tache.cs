using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Apps.Modele
{
    internal class Tache
    {
        private int id { get; set; }
        private string name { get; set; }
        private List<DateTime> times { get; set; }

        public Tache(int _id, string _name, List<DateTime> _times)
        {
            id = _id;
            name = _name;
            times = _times;
        }
    }
}
