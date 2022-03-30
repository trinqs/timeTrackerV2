using Microcharts;
using SkiaSharp;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TimeTracker.Apps.Pages;
using TimeTracker.Apps.WebService;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;

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



		public ConnexionViewModel()
		{
			Login = "";
			Password = "";

			ConnexionAcceuil = new Command(Connexion);
			GoToInscription = new Command(InscriptionNavigation);

		}

		private async void Connexion()
		{
			Boolean connectionReussite = false;
			if ( Password!="" & Login.Contains("@"))
			{
				connectionReussite = await AuthentificationService.Connexion(Login, Password);
			}

			if (connectionReussite)
			{
				Console.WriteLine(connectionReussite.ToString());
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
            else
            {
				await App.Current.MainPage.DisplayToastAsync("Echec de la connexion, vérifier les champs remplies et réessayer", 1000);
			}
		}

		private async void InscriptionNavigation()
		{
			try
			{
				INavigationService navigationService = DependencyService.Get<INavigationService>();
				await navigationService.PushAsync<RegisterView>();
			}
			catch (Exception ex)
			{
				await App.Current.MainPage.DisplayToastAsync("Echec de l'inscription, veuillez", 1000);
				Console.WriteLine(ex.Message);
				if (ex.InnerException != null)
				{
					Console.WriteLine("Inner exception: {0}", ex.InnerException);
				}
			}
		}
	}
}
