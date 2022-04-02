using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Text;
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
    internal class CreerProjetViewModel : ViewModelBase
    {
        private string _text;
        private string _description;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public ICommand NouveauProjet { get; }

        public ICommand CancelCommand { get; }

        public CreerProjetViewModel()
        {
            Text = "";
            Description = "";
            NouveauProjet = new Command(NewProjet);
            CancelCommand = new Command(Cancel);
        }

        private async void  NewProjet()
        {
            String token= Preferences.Get("access_token",null);
            ProjectItem projet= null;

            if(Text!="" & Description != "")
            {
                projet = await ProjetService.AddProject(Text, Description);
            }

            Console.WriteLine(projet.Name + "son nom");

            if (projet != null)
            {
                await App.Current.MainPage.DisplayToastAsync("Projet enregistrée", 1000);
                INavigationService navigationService = DependencyService.Get<INavigationService>();
                await navigationService.PopAsync();
            }
            else if(token!= Preferences.Get("access_token", null))
            {
                NewProjet();
            }
            else
            {
                await App.Current.MainPage.DisplayToastAsync("Echec de l'enregistrement, veuillez vérifier que tout les champs sont bien remplies correctement", 1000);
            }
        }

        private void Cancel()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PopAsync();
        }
    }
}
