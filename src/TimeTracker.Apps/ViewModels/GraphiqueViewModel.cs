using Microcharts;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class GraphiqueViewModel : ViewModelBase
    {

        private PieChart _pieChart;
        public PieChart PieChart
        {
            get => _pieChart;
            set => SetProperty(ref _pieChart, value);
        }

        public GraphiqueViewModel(List<ChartEntry> entries)
        {
            List<ChartEntry> entriesTest = new List<ChartEntry>();
            entriesTest.Add(new ChartEntry(212) { Label = "UWP", ValueLabel = "212"});
            entriesTest.Add(new ChartEntry(248) { Label = "Android", ValueLabel = "248" });
            entriesTest.Add(new ChartEntry(128) { Label = "iOS", ValueLabel = "128"  });
            entriesTest.Add(new ChartEntry(514) { Label = "Shared", ValueLabel = "514" });
            PieChart = new PieChart() { Entries = entriesTest };
        }
    }
}
