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

        private ObservableCollection<TaskItem> _taches;

        public ObservableCollection<TaskItem> Taches
        {
            get => _taches;
            set => SetProperty(ref _taches, value);
        }

        public ICommand Edit { get; }
        public ICommand Supp { get; }

        public ProjetViewModel()
        {
            Edit = new Command(EditerProjet);
            Supp = new Command(SupprimerProjet);

            Taches = new ObservableCollection<TaskItem>();
            afficherTache();
        }

        private void EditerProjet()
        {
        }
        private void SupprimerProjet()
        {
        }

        private async void afficherTache()
        {
            Taches = await TaskService.GetAllTask(165); // A CHANGER
        }
    }
}
