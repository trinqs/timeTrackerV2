using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Apps.ViewModels
{
    internal class CreerProjetViewModel
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

        public ProjetViewModel()
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
