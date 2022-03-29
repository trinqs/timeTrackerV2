using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTracker.Apps.WebService;
using TimeTracker.Dtos.Accounts;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class ProfilViewModel : ViewModelBase
    {

        private String _nameText;
        private String _prenomText;
        private String _emailText;
        private String _mdp;

        private Boolean _readOnly;

        private Boolean _visibilityChangerChamp;
        private Boolean _visibilityEditerProfil;
        private Boolean _visibilityEnregistrerProfil;

        public String NameText 
        {
            get => _nameText;
            set => SetProperty(ref _nameText, value);
        }

        public String PrenomText
        {
            get => _prenomText;
            set => SetProperty(ref _prenomText, value);
        }

        public string EmailText
        {
            get => _emailText;
            set => SetProperty(ref _emailText, value);
        }

        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
        }

        public Boolean ReadOnly
        {
            get => _readOnly;
            set => SetProperty(ref _readOnly, value);
        }

        public Boolean VisibilityChangerChamp
        {
            get => _visibilityChangerChamp;
            set => SetProperty(ref _visibilityChangerChamp, value);
        }

        public Boolean VisibilityEditerProfil
        {
            get => _visibilityEditerProfil;
            set => SetProperty(ref _visibilityEditerProfil, value);
        }

        public Boolean VisibilityEnregistrerProfil
        {
            get => _visibilityEnregistrerProfil;
            set => SetProperty(ref _visibilityEnregistrerProfil, value);

        }

        public ICommand EditerProfil { get; }
        public ICommand EnregistrerProfil { get; }

        public ProfilViewModel ()
        {

            NameText = "Error";
            PrenomText = "Error";
            EmailText = "Error@Error";
            Mdp = "***";

            ChargerProfil();

            ReadOnly = true;

            VisibilityChangerChamp = false;
            VisibilityEditerProfil = true;
            VisibilityEnregistrerProfil = false;

            EditerProfil = new Command(ChangerProfil);
            EnregistrerProfil = new Command(EnregistrerChangement);
        }

        private async void ChargerProfil()
        {
            UserProfileResponse _userInfo = await AccountService.GetProfil();

            NameText = _userInfo.LastName;
            PrenomText = _userInfo.FirstName;
            EmailText = _userInfo.Email;

        }

        private async void EnregistrerChangement()
        {
            UserProfileResponse _userInfo = await AccountService.UpdateProfil(EmailText,PrenomText,NameText);

            NameText = _userInfo.LastName;
            PrenomText = _userInfo.FirstName;
            EmailText = _userInfo.Email;

            ReadOnly = true;

            VisibilityChangerChamp = false;
            VisibilityEditerProfil = true;
            VisibilityEnregistrerProfil = false;
        }

        private async void ChangerProfil ()
        {
            ReadOnly = false;

            VisibilityChangerChamp = true;
            VisibilityEditerProfil = false;
            VisibilityEnregistrerProfil = true;
            
        }
    }
}
