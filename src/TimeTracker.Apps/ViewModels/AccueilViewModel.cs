using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        Stopwatch stopwatch = new Stopwatch();
        private DateTime debut;
        private DateTime fin;
        private String _seconds;

        public String Seconds {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }

        public ICommand VoirProfil { get; }
        public ICommand AjoutProjet { get; }
        public ICommand StartTimer { get; }
        public ICommand StopTimer { get; }

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

            StartTimer = new Command(Start);
            StopTimer = new Command(Stop);
            Seconds = stopwatch.Elapsed.Seconds.ToString();

            Projets = new ObservableCollection<ProjectItem>();
            getProjets();
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

        private async void Start()
        {
            debut = DateTime.Today;
        }

        private async void Stop()
        {
            fin = DateTime.Today;
        }

        private async void getProjets()
        {
            Projets = await ProjetService.GetAllProject();
        }
    }
}
