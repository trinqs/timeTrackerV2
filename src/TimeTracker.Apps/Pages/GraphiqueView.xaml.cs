
using Microcharts;
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
    public partial class GraphiqueView : ContentPage
    {
        public GraphiqueView(List<ChartEntry> entries)
        {
            InitializeComponent();
            BindingContext = new GraphiqueViewModel(entries);
        }
    }
}