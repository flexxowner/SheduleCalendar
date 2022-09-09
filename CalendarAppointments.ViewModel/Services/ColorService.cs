using CalendarAppointments.Models.Models;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CalendarAppointments.ViewModel.Service
{
    public static class ColorService
    {
        private static Brush greenColor = new SolidColorBrush(Colors.Green);
        private static Brush redColor = new SolidColorBrush(Colors.Red);

        public static void AddColor(IEnumerable<YearDay> days)
        {
            foreach (var item in days)
            {
                item.Color = greenColor;
            }
        }

        public static void AddColorForToday(ObservableCollection<YearDay> days)
        {
            foreach (var item in days)
            {
                if (item.Date.Date == DateTime.Today.Date)
                {
                    item.Color = redColor;
                }
            }
        }
    }
}
