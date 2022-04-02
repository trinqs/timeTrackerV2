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
        }

        async private void ProjetSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ProjetView((Dtos.Projects.ProjectItem)e.Item));
        }
    }
}