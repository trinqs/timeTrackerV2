using Microcharts;
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
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;

namespace TimeTracker.Apps.ViewModels
{
    internal class AccueilViewModel : ViewModelBase
    {
        Stopwatch stopwatch = new Stopwatch();
        
        private String _seconds;

        public String Seconds {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
            
        }

        public ICommand VoirProfil { get; }
        public ICommand AjoutProjet { get; }
        public ICommand StartTimer { get; }
        public ICommand StopTimer { get; }
        public ICommand VoirGraphique { get; }

        public ICommand ItemTappedCommand { get; }

        private ObservableCollection<ProjectItem> _projets;
        public ObservableCollection<ProjectItem> Projets
        {
            get => _projets;
            set => SetProperty(ref _projets, value);
        }

        public AccueilViewModel()
        {
            VoirProfil = new Command(GoToProfil);
            AjoutProjet = new Command(GoToCreerProjet);

            StartTimer = new Command(Start);
            StopTimer = new Command(Stop);
            Seconds = TimeSpan.Zero.ToString(@"hh\:mm\:ss");

            VoirGraphique = new Command(GoToGraphique);
            ItemTappedCommand = new Command<ProjectItem>(ItemTappedHandler);

            Projets = new ObservableCollection<ProjectItem>();
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            getProjets();
            if (Preferences.Get("timerEnCours", true) == false)
            {
                Preferences.Remove("depart");
                Preferences.Remove("fin");
                Seconds = TimeSpan.Zero.ToString(@"hh\:mm\:ss");
            }
            else
            {
                Device.StartTimer(new TimeSpan(0, 0, 1),
                    () =>
                    {
                        if (Preferences.Get("timerEnCours", false) == true)
                        {
                            Seconds = (DateTime.Now - Preferences.Get("depart", DateTime.Now)).ToString(@"hh\:mm\:ss");
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
            }
        }

        private async void ItemTappedHandler(ProjectItem projectItem)
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PushAsync(new ProjetView(projectItem.Id));
        }

        private async void GoToProfil()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PushAsync<ProfilView>();
        }

        private async void GoToCreerProjet()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PushAsync<CreerProjetView>();
        }

        private async void Start()
        {
            if (Preferences.Get("timerEnCours", false) == false)
            {
                Preferences.Set("timerEnCours", true);
                Preferences.Set("depart", DateTime.Now);
                Device.StartTimer(new TimeSpan(0, 0, 1), 
                    () =>
                 {
                     if (Preferences.Get("timerEnCours", false) == true)
                     {
                         Seconds = (DateTime.Now - Preferences.Get("depart", DateTime.Now)).ToString(@"hh\:mm\:ss");
                         return true;
                     }
                     else
                     {
                         return false;
                     }
                 });
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Timer déjà en cours", 3000);
            }
            
        }

        private async void Stop()
        {
            if (Preferences.Get("timerEnCours", false) == true)
            {
                Preferences.Set("timerEnCours", false);
                Preferences.Set("fin", DateTime.Now);
                if (Preferences.ContainsKey("idProjet") && Preferences.ContainsKey("idTache"))
                {
                    long idProjet = Preferences.Get("idProjet", long.MinValue);
                    long idTache = Preferences.Get("idTache", long.MinValue);
                    Preferences.Remove("idProjet");
                    Preferences.Remove("idTache");
                    await TimeService.AddTime(idProjet,idTache,
                                        Preferences.Get("depart",DateTime.MinValue),
                                        Preferences.Get("fin",DateTime.MinValue));
                    
                }
                else
                {
                    INavigationService navigationService = DependencyService.Get<INavigationService>();
                    await navigationService.PushAsync<CreerTempsView>();
                }            
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Veuillez démarrer un timer", 3000);
            }
        }

        private async void getProjets()
        {
            Projets = await ProjetService.GetAllProject();
            OnPropertyChanged(nameof(Projets));
        }

        private async void GoToGraphique()
        {
            List<ChartEntry> chartEntries = new List<ChartEntry>();
            foreach (ProjectItem projet in Projets)
            {
                chartEntries.Add(new ChartEntry(projet.TotalSeconds) { Label = projet.Name , ValueLabel = projet.TotalSeconds.ToString()});
            }

            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PushAsync(new GraphiqueView(chartEntries));

        }
    }
}
