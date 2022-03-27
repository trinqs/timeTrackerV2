﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Apps.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Apps.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnexionView : ContentPage
    {
        public ConnexionView()
        {
            InitializeComponent();
            BindingContext = new ConnexionViewModel();
        }
    }
}