﻿using System;
using TimeTracker.Apps.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Apps.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreerTempsView : ContentPage
    {
        public CreerTempsView()
        {          
            InitializeComponent();           
            BindingContext = new CreerTempsViewModel();
        }
    }
}