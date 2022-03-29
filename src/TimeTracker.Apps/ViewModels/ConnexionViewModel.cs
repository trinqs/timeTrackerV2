using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TimeTracker.Apps.Pages;
using TimeTracker.Apps.WebService;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
	internal class ConnexionViewModel : ViewModelBase
	{
		private String _login;
		private String _password;

		public string Login
		{
			get => _login;
			set => SetProperty(ref _login, value);
		}

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		public ICommand ConnexionAcceuil { get; }

		public ICommand GoToInscription { get; }

		public ICommand GraphTest { get; }

		public ConnexionViewModel()
		{
			ConnexionAcceuil = new Command(Connexion);
			GoToInscription = new Command(Inscription);
			GraphTest = new Command(async () =>
		    {
			    INavigationService navigationService = DependencyService.Get<INavigationService>();
			    await navigationService.PushAsync<GraphiqueView>(null, NavigationMode.ReplaceAll);
		    });
		}

		private async void Connexion()
		{
			await AuthentificationService.Connexion(Login, Password);

			
			try
			{
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

		private async void Inscription()
		{
			try
			{
				INavigationService navigationService = DependencyService.Get<INavigationService>();
				await navigationService.PushAsync<RegisterView>();
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
