using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class ProfilViewModel : ViewModelBase
    {

        private String _name;
        private String _prenom;
        private String _email;
        private String _mdp;

        public String Name 
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public String Prenom
        {
            get => _prenom;
            set => SetProperty(ref _prenom, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
        }

        public ICommand EditerProfil { get; }

        public ProfilViewModel ()
        {
            _name = "moi";
            _prenom = "moi";
            _email = "moi@moi";
            _mdp = "***";

            EditerProfil = new Command(ChangerProfil);
        }

        private void ChangerProfil ()
        {

        }
    }
}
