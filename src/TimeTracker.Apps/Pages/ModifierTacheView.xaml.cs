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
    public partial class ModifierTacheView : BaseContentPage
    {
        public ModifierTacheView(long idProjet, long idtache)
        {
            InitializeComponent();
            BindingContext = new ModifierTacheViewModel(idProjet, idtache);
        }
    }
}