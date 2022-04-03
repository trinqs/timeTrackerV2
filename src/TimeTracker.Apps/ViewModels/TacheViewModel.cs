using Microcharts;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTracker.Apps.Pages;
using TimeTracker.Apps.WebService;
using TimeTracker.Dtos.Projects;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class TacheViewModel : ViewModelBase
    {
        private long _idProjet;
        private long _idTache;
        private string _seconds;
        private TimeItem _timeItem;

        private TaskItem _tache;

        private List<TimeItem> _temps;

        public ICommand SupprimerTache { get; }
        public ICommand EditerTache { get; }
        public ICommand CommencerTimer { get; }
        public ICommand ArreterTimer { get; }
        public ICommand ItemSelectedCommand { get; }
        public ICommand SupprimerTempsCommand { get; }


        public long IdProjet
        {
            get => _idProjet;
            set => SetProperty(ref _idProjet, value);
        }

        public long IdTache { 
            get => _idTache;
            set => SetProperty(ref _idTache, value);
        } 

        public TaskItem Tache
        {
            get => _tache;
            set => SetProperty(ref _tache, value);
        }

        public List<TimeItem> Temps
        {
            get => _temps;
            set => SetProperty(ref _temps, value);
        }

        public TimeItem TimeItem
        {
            get => _timeItem;
            set => SetProperty(ref _timeItem, value);
        }

        public TacheViewModel(long projetId, long tacheId)
        {
            IdProjet = projetId;
            IdTache = tacheId;
            Temps = new List<TimeItem>();
            Seconds = TimeSpan.Zero.ToString(@"hh\:mm\:ss");
            SupprimerTache = new Command(DeleteTache);
            EditerTache = new Command(EditTache);
            CommencerTimer = new Command(Start);
            ArreterTimer = new Command(Stop);
            ItemSelectedCommand = new Command<TimeItem>(ItemSelectedHandler);
            SupprimerTempsCommand = new Command(SupprimerTemps);

        }
       public override async Task OnResume()
        {
            await base.OnResume();
            AfficherTime();
            
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

        private async void DeleteTache()
        {
            await TaskService.DeleteTask(IdProjet,IdTache);
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PopAsync();

        }

        private async void EditTache()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PushAsync(new ModifierTacheView(IdProjet, IdTache));
        }

        private void ItemSelectedHandler(TimeItem timeItemArg)
        {
            TimeItem = timeItemArg;
        }

        private async void SupprimerTemps()
        {
            await TimeService.DeleteTime(IdProjet, IdTache, TimeItem.Id);
            await OnResume();
        }



        private async void AfficherTime()
        {
            Console.WriteLine("Hellooooooo");
            Tache = await TaskService.GetTaskById(IdProjet, IdTache);
            Console.WriteLine("Je suis la "+Tache.Name);
            Temps = Tache.Times;
        }
        
        public string Seconds
        {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }

        
        private async void Start()
        {
            if (Preferences.Get("timerEnCours", false) == false)
            {
                Preferences.Set("timerEnCours", true);
                Preferences.Set("depart", DateTime.Now);
                Preferences.Set("idProjet", IdProjet);
                Preferences.Set("idTache", IdTache);
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
                Preferences.Remove("idProjet");
                Preferences.Remove("idTache");
                await TimeService.AddTime(IdProjet, IdTache,
                                     Preferences.Get("depart", DateTime.MinValue),
                                     Preferences.Get("fin", DateTime.MinValue));
                await OnResume();
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Veuillez démarrer un timer", 3000);
            }
        }

    }
}
