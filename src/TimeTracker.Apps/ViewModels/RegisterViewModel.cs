using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TimeTracker.Apps.Pages;
using TimeTracker.Apps.WebService;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class RegisterViewModel : ViewModelBase
    {
        private string _login;       
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        
        
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        
        
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public ICommand InscriptionAccueil { get; }

        public RegisterViewModel()
        {
            InscriptionAccueil = new Command(Inscription);
        }

        private async void Inscription()
        {
            try
            {
                //await AuthentificationService.Inscription(Login, FirstName, LastName, Password);
                INavigationService navigationService = DependencyService.Get<INavigationService>();
                await navigationService.PushAsync<AccueilView>(null, NavigationMode.ReplaceAll);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", ex.InnerException);
                }
            }

        }


    }
}
