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
    internal class ModifierProjetViewModel : ViewModelBase
    {
        private ProjectItem _projet;
        private string _nameProjet;
        private string _descriptionProjet;

        public ProjectItem Projet
        {
            get => _projet;
            set => SetProperty(ref _projet, value);
        }
        public string NameProjet
        {
            get => _nameProjet;
            set => SetProperty(ref _nameProjet, value); 
        }
        public string DescriptionProjet
        {
            get => _descriptionProjet;
            set => SetProperty(ref _descriptionProjet, value);
        }

        public ICommand ModifierProjet { get; }

        public ModifierProjetViewModel(ProjectItem projet)
        {
            Projet = projet;
            NameProjet = Projet.Name;
            DescriptionProjet = Projet.Description;
            ModifierProjet = new Command(EnregistrerModification);
        }

        public async void EnregistrerModification()
        {
            String token = Preferences.Get("access_token", null);
            ProjectItem _userInfo = null;

            if (NameProjet != "" & DescriptionProjet!= "")
            {
                _userInfo = await ProjetService.UpdateProject(NameProjet, DescriptionProjet, Projet.Id);
            }

            if (_userInfo != null)
            {
                await App.Current.MainPage.DisplayToastAsync("Modification Effectué", 1000);
                INavigationService navigationService = DependencyService.Get<INavigationService>();
                await navigationService.PopAsync();

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
    }
}
