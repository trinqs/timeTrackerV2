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
    public partial class ModifierProjet : ContentPage
    {
        public ModifierProjet(ProjectItem projet)
        {
            InitializeComponent();
            BindingContext = new ModifierProjetViewModel(projet);
        }
    }
}