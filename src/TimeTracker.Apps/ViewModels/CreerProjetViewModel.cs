using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TimeTracker.Apps.Modele;
using TimeTracker.Apps.Pages;
using TimeTracker.Apps.WebService;
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
            NouveauProjet = new Command(NewProjet);
            CancelCommand = new Command(Cancel);
        }

        private void NewProjet()
        {
            string text = Text;
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var projetService = DependencyService.Get<ProjetService>();
            //projetService.AddProject(Text, Description);
            //projetService.addProjet(Text, Description);

            new Projet(0, _text, _description, 0);
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PushAsync<AccueilView>();
        }

        private void Cancel()
        {
            INavigationService navigationService = DependencyService.Get<INavigationService>();
            navigationService.PushAsync<AccueilView>();
        }
    }
}
