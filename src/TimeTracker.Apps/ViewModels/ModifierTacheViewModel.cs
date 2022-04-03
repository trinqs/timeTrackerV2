using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
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
    internal class ModifierTacheViewModel : ViewModelBase
    {
        private long _idProjet;
        private long _idTache;

        private string _nameTache;
        private TaskItem _tache;

        public ICommand EnregistrerModification { get; }

        public long IdProjet
        {
            get => _idProjet;
            set => SetProperty(ref _idProjet, value);
        }
        public long IdTache
        {
            get => _idTache;
            set => SetProperty(ref _idTache, value);
        }
        public string NameTache
        {
            get => _nameTache;
            set => SetProperty(ref _nameTache, value);
        }
        public TaskItem Tache
        {
            get => _tache;
            set => SetProperty(ref _tache, value);
        }
        

        public ModifierTacheViewModel(long idProjet, long tache)
        {
            IdProjet = idProjet;
            IdTache = tache;
            afficherTache();
            EnregistrerModification = new Command(UpdateTache);
        }

        private async void afficherTache()
        {
            Tache = await TaskService.GetTaskById(IdProjet, IdTache);
            NameTache = Tache.Name;
        }

        public async void UpdateTache()
        {
            String token = Preferences.Get("access_token", null);
            TaskItem tacheInfo = null;

            if (NameTache !="")
            {
                tacheInfo = await TaskService.UpdateTask(NameTache, IdTache, IdProjet);
            }

            if (tacheInfo != null)
            {
                await App.Current.MainPage.DisplayToastAsync("Modification Effectué", 1000);
                INavigationService navigationService = DependencyService.Get<INavigationService>();
                await navigationService.PopAsync();

            }
            else if (token != Preferences.Get("access_token", null))
            {
                UpdateTache();
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Un problème est survenu, assuré vous que vous avez tout bien remplie correctement", 1000);
            }
        }
    }
}
