using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TimeTracker.Apps.WebService;
using TimeTracker.Dtos.Projects;

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

        public CreerTempsViewModel()
        {
            Projets = new ObservableCollection<ProjectItem>();
            Taches = new ObservableCollection<TaskItem>();
            getProjets();
        }

        private async void getProjets()
        {
            Projets = await ProjetService.GetAllProject();
            OnPropertyChanged(nameof(Projets));
        }

    }
}
