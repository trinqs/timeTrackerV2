using Storm.Mvvm.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Apps.Modele;
using TimeTracker.Apps.ViewModels;
using TimeTracker.Dtos.Projects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Apps.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjetView : BaseContentPage
    {
        public ProjetView(int idProjet)
        {
            InitializeComponent();
            BindingContext = new ProjetViewModel(idProjet);
        }

        /*private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }*/

        async private void ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            //await Navigation.PushAsync(BindinIdProjet, new TacheView((Dtos.Projects.TaskItem)e.Item));
            //((ListView)sender).SelectedItem = null;
        }
    }
}