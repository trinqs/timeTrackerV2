using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Storm.Mvvm;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    public class ListProjetViewModel : NotifierBase
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ListProjetViewModel()
        {

        }
    }
}