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
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class ProjetViewModel : ViewModelBase
    {
        private int _idProjet;
        private ProjectItem _projet;
        private ObservableCollection<TaskItem> _taches;

        private string _projectName;
        private string _projectDescription;

        public int IdProjet
        {
            get => _idProjet;
            set => SetProperty(ref _idProjet, value);
        }

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

        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }
        public string ProjetDescription
        {
            get => _projectDescription;
            set => SetProperty(ref _projectDescription, value);
        }

        public ICommand Edit { get; }
        public ICommand Supp { get; }
        public ICommand AjouterTache { get; }
        public ICommand ItemTappedCommand { get; }

        public ProjetViewModel(int idProjet)
        {
            IdProjet = idProjet;

            Edit = new Command(EditerProjet);
            Supp = new Command(SupprimerProjet);
            AjouterTache = new Command(AddTache);
            ItemTappedCommand = new Command<TaskItem>(ItemTappedHandler);

            Taches = new ObservableCollection<TaskItem>();
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            afficherTache();
        }

        private async void EditerProjet()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PushAsync(new ModifierProjet(Projet));
        }

        private async void SupprimerProjet()
        {
            await ProjetService.DeleteProject((int)Projet.Id);
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PopAsync();
        }

        private async void AddTache()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PushAsync(new CreerTacheView((int)Projet.Id));
        }

        private async void ItemTappedHandler(TaskItem tacheItem)
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PushAsync(new TacheView(IdProjet,(int)tacheItem.Id));
        }

        private async void afficherTache()
        {
            Projet = await ProjetService.GetProjetById(IdProjet);

            ProjectName = Projet.Name;
            ProjetDescription = Projet.Description;

            Taches = await TaskService.GetAllTask((int)Projet.Id);
        }
    }
}
