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
using TimeTracker.Apps.WebService;
using TimeTracker.Dtos.Projects;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class AccueilViewModel : ViewModelBase
    {

        public ICommand VoirProfil { get; }
        public ICommand AjoutProjet { get; }

        private ObservableCollection<ProjectItem> _projets;
        public ObservableCollection<ProjectItem> Projets
        {
            get => _projets;
            set => SetProperty(ref _projets, value);
        }

        public AccueilViewModel()
        {
            VoirProfil = new Command(GoToProfil);
            AjoutProjet = new Command(GoToProjet);
            Projets = new ObservableCollection<ProjectItem>();
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

        private async void creerJeuTest()
        {
            Projets = await ProjetService.GetAllProject();
        }
    }
}
