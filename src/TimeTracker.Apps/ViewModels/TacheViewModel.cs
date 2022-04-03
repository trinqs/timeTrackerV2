using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private TaskItem _tache;

        private List<TimeItem> _temps;

        public ICommand SupprimerTache { get; }
        public ICommand EditerTache { get; }
        public ICommand CommencerTimer { get; }
        public ICommand ArreterTimer{ get; }

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

        public TacheViewModel(long projetId, long tacheId)
        {
            IdProjet = projetId;
            IdTache = tacheId;
            Temps = new List<TimeItem>();
            SupprimerTache = new Command(DeleteTache);
            //EditerTache = new Command(DeleteTache);
            CommencerTimer = new Command(Start);
            ArreterTimer = new Command(Stop);
        }
       public override async Task OnResume()
        {
            await base.OnResume();
            afficherTime();
        }

        private async void DeleteTache()
        {
            await TaskService.DeleteTask(IdProjet,IdTache);
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PopAsync();

        }

 

        private async void afficherTime()
        {
            Console.WriteLine("Hellooooooo");
            Tache = await TaskService.GetTaskById(IdProjet, IdTache);
            Console.WriteLine("Je suis la "+Tache.Name);
            Temps = Tache.Times;
        }



        private string _seconds;
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
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Veuillez démarrer un timer", 3000);
            }
        }

    }
}
