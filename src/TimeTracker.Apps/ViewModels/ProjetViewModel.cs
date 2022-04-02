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
    internal class ProjetViewModel : ViewModelBase
    {

        private ProjectItem _projet;

        private ObservableCollection<TaskItem> _taches;


        public ProjectItem Projet
        {
            get => _projet;
            set => SetProperty(ref _projet, value);
        }

        public ObservableCollection<TaskItem> Taches
        {
            get => _taches;
            set => SetProperty(ref _taches, value);
        }

        public ICommand Edit { get; }
        public ICommand Supp { get; }

        public ProjetViewModel(ProjectItem projet)
        {
            Projet = projet;

            Edit = new Command(EditerProjet);
            Supp = new Command(SupprimerProjet);

            Taches = new ObservableCollection<TaskItem>();
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            afficherTache();
        }

        private void EditerProjet()
        {
        }
        private async void SupprimerProjet()
        {
            await ProjetService.DeleteProject((int)Projet.Id);
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PopAsync();

        }

        private async void afficherTache()
        {
            Taches = await TaskService.GetAllTask((int)Projet.Id);
        }
    }
}
