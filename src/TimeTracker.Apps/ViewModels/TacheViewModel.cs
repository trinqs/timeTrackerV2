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
            CommencerTimer = new Command(DeleteTache);

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

    }
}
