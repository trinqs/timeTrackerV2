using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TimeTracker.Apps.WebService;
using TimeTracker.Dtos.Projects;
using Xamarin.Forms;
using Xamarin.Essentials;
using Storm.Mvvm.Services;

namespace TimeTracker.Apps.ViewModels
{
    internal class CreerTempsViewModel : ViewModelBase
    {

        private ObservableCollection<ProjectItem> _projets;
        public ObservableCollection<ProjectItem> Projets
        {
            get => _projets;
            set => SetProperty(ref _projets, value);
        }


        private ObservableCollection<TaskItem> _taches;
        public ObservableCollection<TaskItem> Taches
        {
            get => _taches;
            set => SetProperty(ref _taches, value);
        }

        private ProjectItem _project;
        public ProjectItem Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        private bool _projetChoisiBool=false;
        public bool ProjetChoisiBool
        {
            get => _projetChoisiBool;
            set => SetProperty(ref _projetChoisiBool, value);
        }

        public ICommand ProjetSelectionneCommand { get; }
        public ICommand TachesSelectionneCommand { get; }

        public CreerTempsViewModel()
        {
            Console.WriteLine("Dans le viewModel");
            Projets = new ObservableCollection<ProjectItem>();
            Taches = new ObservableCollection<TaskItem>();
            ProjetSelectionneCommand = new Command<ProjectItem>(ProjetSelectionne);
            TachesSelectionneCommand = new Command<TaskItem>(TacheSelectionne);
            GetProjets();
        }

        private async void ProjetSelectionne(ProjectItem projectItem)
        {   
            Taches = await TaskService.GetAllTask((int)projectItem.Id);
            Project = projectItem;
            ProjetChoisiBool = true;
        }

        private async void TacheSelectionne(TaskItem taskItem)
        {
            Console.WriteLine("Avant le addtime");
            await TimeService.AddTime( (int)Project.Id, (int) taskItem.Id, Preferences.Get("depart",DateTime.MinValue), Preferences.Get("fin",DateTime.MinValue) );
            Console.WriteLine("Après le addTime");
            Preferences.Remove("depart");
            Preferences.Remove("fin");
            Preferences.Set("timerEnCours", false);
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            await navigationService.PopAsync();
        }

        private async void GetProjets()
        {
            Projets = await ProjetService.GetAllProject();
            OnPropertyChanged(nameof(Projets));
        }

    }
}
