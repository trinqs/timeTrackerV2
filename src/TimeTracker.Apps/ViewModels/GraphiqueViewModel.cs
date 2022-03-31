using Microcharts;
using SkiaSharp;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    internal class GraphiqueViewModel : ViewModelBase
    {

        private DonutChart _donutChart;
        public DonutChart DonutChart
        {
            get => _donutChart;
            set => SetProperty(ref _donutChart, value);
        }

        public GraphiqueViewModel(List<ChartEntry> entries)
        {
            foreach (ChartEntry entry in entries)
            {
                entry.Color = colorGenerator();
            }
            DonutChart = new DonutChart() {
                LabelTextSize = 50,
                Entries = entries
            };
        }

        //methode pour générer aléatoirement des couleurs pour le graphique, il y a un risque que les couleurs soient proches mais il est relativement faible
        private static SKColor colorGenerator()
        {
            SKColor color = SKColor.Empty;
            Random random = new Random();
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = random.Next(256);
            string redHex;
            string greenHex;
            string blueHex;
 
            if (red < 16)
            {
                redHex = "0" + red.ToString("X");
            }
            else
            {
                redHex = red.ToString("X");
            }
            
            if (green < 16)
            {
                greenHex = "0" + green.ToString("X");
            }
            else
            {
                greenHex = green.ToString("X");
            }
            
            if (blue < 16)
            {
                blueHex = "0" + blue.ToString("X");
            }
            else
            {
                blueHex = blue.ToString("X");
            }

            try
            {
                color = SKColor.Parse("#" + redHex + greenHex + blueHex);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("red:" + red + ", hex:" + red.ToString("X"));
                Console.WriteLine("green:" + green + ", hex:" + green.ToString("X"));
                Console.WriteLine("blue:" + blue + ", hex:" + blue.ToString("X"));
                Console.WriteLine(ex.Message);
            }
           
            return color;
        }
    }
}
