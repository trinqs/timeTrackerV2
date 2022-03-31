using System;
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
    public partial class AccueilView : ContentPage
    {
        public AccueilView()
        {
            InitializeComponent();
            BindingContext = new AccueilViewModel();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}