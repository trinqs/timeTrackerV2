using Storm.Mvvm.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Apps.ViewModels;
using TimeTracker.Dtos.Projects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Apps.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjetView : BaseContentPage
    {
        public ProjetView(ProjectItem projet)
        {
            InitializeComponent();
            BindingContext = new ProjetViewModel(projet);
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}