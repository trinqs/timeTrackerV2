using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TimeTracker.Apps.WebService;
using TimeTracker.Dtos.Projects;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class CreerTacheViewModel : ViewModelBase
    {
        private string _nomTache;
        private int _idProjet;

        public string NomTache
        {
            get => _nomTache;
            set => SetProperty(ref _nomTache, value);
        }

        public int IdProjet
        {
            get => _idProjet;
            set => SetProperty(ref _idProjet, value);
        }

        public ICommand NouveauProjet { get; }

        public CreerTacheViewModel(int idProjet)
        {
            IdProjet = idProjet;
            NomTache = "";
            NouveauProjet = new Command(AjouterProjet);
        }


        private async void AjouterProjet()
        {
            String token = Preferences.Get("access_token", null);
            TaskItem tache = null;

            if (NomTache != "")
            {
                tache = await TaskService.AddTask(NomTache, IdProjet);
            }


            if (tache != null)
            {
                await App.Current.MainPage.DisplayToastAsync("Tache enregistrée", 1000);
                INavigationService navigationService = DependencyService.Get<INavigationService>();
                await navigationService.PopAsync();
            }
            else if (token != Preferences.Get("access_token", null))
            {
                AjouterProjet();
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Echec de l'enregistrement, veuillez vérifier que tout les champs sont bien remplies correctement", 1000);
            }
        }
    }
}
