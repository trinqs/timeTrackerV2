using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTracker.Dtos.Projects;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class TacheViewModel : ViewModelBase
    {
        private TaskItem _tache;

        private List<TimeItem> _temps;

        public ICommand SupprimerTache { get; }
        public ICommand EditerTache { get; }
        public ICommand CommencerTimer { get; }

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

        public TacheViewModel(TaskItem tache)
        {
            Console.WriteLine(tache.Name);
            Tache = tache;
            SupprimerTache = new Command(DeleteTache);
            EditerTache = new Command(DeleteTache);
            CommencerTimer = new Command(DeleteTache);

        }

        private async void DeleteTache()
        {
            
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            afficherTime();
        }

        private async void afficherTime()
        {
            Console.WriteLine(Tache.Times[0]);
            Console.WriteLine(Tache.Times[1]);
            Temps = Tache.Times;
        }

    }
}
