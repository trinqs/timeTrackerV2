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

        private ProjectItem _projet;
        private ObservableCollection<TaskItem> _taches;

        private string _projectName;
        private string _projectDescription;

        private bool _readOnlyLabel;
        private bool _isVisibleEditerButton;
        private bool _isVisibleEnregistrerButtonAndLabel;


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
        public bool ReadOnlyLabel
        {
            get => _readOnlyLabel;
            set => SetProperty(ref _readOnlyLabel, value);
        }
        public bool IsVisibleEditerButton
        {
            get => _isVisibleEditerButton;
            set => SetProperty(ref _isVisibleEditerButton, value);
        }
        public bool IsVisibleEnregistrerButtonAndLabel
        {
            get => _isVisibleEnregistrerButtonAndLabel;
            set => SetProperty(ref _isVisibleEnregistrerButtonAndLabel, value);
        }

        public ICommand Edit { get; }
        public ICommand Supp { get; }
        public ICommand AjouterTache { get; }
        public ICommand EnregistrerChangement { get; }

        public ProjetViewModel(ProjectItem projet)
        {
            Projet = projet;
            ReadOnlyLabel =true;
            IsVisibleEditerButton = true;
            IsVisibleEnregistrerButtonAndLabel = false;
            ProjectName = Projet.Name;
            ProjetDescription = Projet.Description;

            Edit = new Command(EditerProjet);
            Supp = new Command(SupprimerProjet);
            AjouterTache = new Command(AddTache);
            EnregistrerChangement = new Command(EnregistrerModification);

            Taches = new ObservableCollection<TaskItem>();
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            afficherTache();
        }

        private void EditerProjet()
        {
            ReadOnlyLabel = false;
            IsVisibleEditerButton = false;
            IsVisibleEnregistrerButtonAndLabel = true;
        }

        private async void EnregistrerModification()
        {
            String token = Preferences.Get("access_token", null);
            ProjectItem _userInfo = null;

            if (ProjectName != "" & ProjetDescription != "")
            {
                _userInfo = await ProjetService.UpdateProject(ProjectName,ProjetDescription, (int)Projet.Id);
            }

            if (_userInfo != null)
            {
                ReadOnlyLabel = true;
                IsVisibleEditerButton = true;
                IsVisibleEnregistrerButtonAndLabel = false;
                await App.Current.MainPage.DisplayToastAsync("Enregistrement Effectué", 1000);
            }
            else if (token != Preferences.Get("access_token", null))
            {
                EnregistrerModification();
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Un problème est survenu, assuré vous que vous avez tout bien remplie correctement", 1000);
            }
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

        private async void afficherTache()
        {
            Taches = await TaskService.GetAllTask((int)Projet.Id);
        }
    }
}
