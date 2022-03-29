using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Apps.Modele
{
    internal class Projet
    {
        private int id { get; set; }
        private string name { get; set; }
        private string description { get; set; }
        private int total_seconds { get; set; }

        public Projet(int _id, string _name, string _description, int _total_seconds)
        {
            id = _id;
            name = _name;
            description = _description;
            total_seconds = _total_seconds;
        }

    }
}
