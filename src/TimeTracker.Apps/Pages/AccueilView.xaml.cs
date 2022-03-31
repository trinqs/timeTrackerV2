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

        async private void ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            //var item = e.Item as Projets;
            await Navigation.PushAsync(new ProjetView((Dtos.Projects.ProjectItem)e.Item));
        }
    }
}