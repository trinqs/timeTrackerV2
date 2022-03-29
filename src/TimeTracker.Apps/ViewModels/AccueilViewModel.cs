using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTracker.Apps.Modele;
using TimeTracker.Apps.Pages;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class AccueilViewModel : ViewModelBase
    {

        public ICommand VoirProfil { get; }
        public ICommand AjoutProjet { get; }

        private ObservableCollection<Projet> _projets;
        public ObservableCollection<Projet> Projets
        {
            get => _projets;
            set => SetProperty(ref _projets, value);
        }

        public AccueilViewModel()
        {
            VoirProfil = new Command(GoToProfil);
            AjoutProjet = new Command(GoToProjet);
            Projets = new ObservableCollection<Projet>();
            creerJeuTest();
        }

        private void GoToProfil()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PushAsync<ProfilView>();
        }

        private void GoToProjet()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PushAsync<ProjetView>();
        }

        private void creerJeuTest()
        {
            Projets.Add(new Projet(0, "Projet1", "Le projet 1", 0));
            Projets.Add(new Projet(1, "Projet2", "Le projet 2", 0));
            Projets.Add(new Projet(2, "Projet3", "Le projet 3", 0));
        }
    }
}
