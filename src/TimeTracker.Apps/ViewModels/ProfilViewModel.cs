﻿using Storm.Mvvm;
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
        private String _ancientMotDePasse;
        private String _nouveauMotDePasse;

        private Boolean _readOnly;

        private Boolean _visibilityChangerChamp;
        private Boolean _visibilityEditerProfil;
        private Boolean _visibilityEnregistrerProfil;
        private Boolean _visibilityModifierMotDePasse;
        private Boolean _visibilityEnregistrerMotDePasse;
        private Boolean _visibilityGridMotDePasse;

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

        public string AncientMotDePasse
        {
            get => _ancientMotDePasse;
            set => SetProperty(ref _ancientMotDePasse, value);
        }

        public string NouveauMotDePasse
        {
            get => _nouveauMotDePasse;
            set => SetProperty(ref _nouveauMotDePasse, value);
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

        public Boolean VisibilityModifierMotDePasse
        {
            get => _visibilityModifierMotDePasse;
            set => SetProperty(ref _visibilityModifierMotDePasse, value);
        }

        public Boolean VisibilityEnregistrerMotDePasse
        {
            get => _visibilityEnregistrerMotDePasse;
            set => SetProperty(ref _visibilityEnregistrerMotDePasse, value);
        }

        public Boolean VisibilityGridMotDePasse
        {
            get => _visibilityGridMotDePasse;
            set => SetProperty(ref _visibilityGridMotDePasse, value);
        }

        public ICommand EditerProfil { get; }
        public ICommand EnregistrerProfil { get; }
        public ICommand EditerMotDePasse { get; }
        public ICommand EngistrerMotDePasse { get; }


        public ProfilViewModel ()
        {

            NameText = "Error";
            PrenomText = "Error";
            EmailText = "Error@Error";
            AncientMotDePasse = "";
            NouveauMotDePasse = "";

            ChargerProfil();

            ReadOnly = true;

            VisibilityChangerChamp = false;
            VisibilityEditerProfil = true;
            VisibilityEnregistrerProfil = false;
            VisibilityModifierMotDePasse = true;
            VisibilityGridMotDePasse = false;
            VisibilityEnregistrerMotDePasse= false;

            EditerProfil = new Command(ChangerProfil);
            EnregistrerProfil = new Command(EnregistrerChangementProfil);
            EditerMotDePasse = new Command(ChangerMotDePasse);
            EngistrerMotDePasse = new Command(EnregistrerChangementMotDePasse);
        }

        private async void ChargerProfil()
        {
            UserProfileResponse _userInfo = await AccountService.GetProfil();

            NameText = _userInfo.LastName;
            PrenomText = _userInfo.FirstName;
            EmailText = _userInfo.Email;

        }

        private async void EnregistrerChangementProfil()
        {
            UserProfileResponse _userInfo = await AccountService.UpdateProfil(EmailText,PrenomText,NameText);

            NameText = _userInfo.LastName;
            PrenomText = _userInfo.FirstName;
            EmailText = _userInfo.Email;

            ReadOnly = true;

            VisibilityChangerChamp = false;
            VisibilityEditerProfil = true;
            VisibilityEnregistrerProfil = false;

            ChargerProfil();
        }

        private void ChangerProfil ()
        {
            ReadOnly = false;

            VisibilityChangerChamp = true;
            VisibilityEditerProfil = false;
            VisibilityEnregistrerProfil = true;
            
        }
        private void ChangerMotDePasse(object obj)
        {
            VisibilityModifierMotDePasse = false;
            VisibilityGridMotDePasse = true;
            VisibilityEnregistrerMotDePasse = true;
        }

        private async void EnregistrerChangementMotDePasse(object obj)
        {
            bool _userInfo = await AccountService.SetPassword(AncientMotDePasse, NouveauMotDePasse);
            VisibilityModifierMotDePasse = true;
            VisibilityGridMotDePasse = false;
            VisibilityEnregistrerMotDePasse = false;
        }

       
    }
}
