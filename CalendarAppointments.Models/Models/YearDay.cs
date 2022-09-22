using System;
using Windows.UI.Xaml.Media;

namespace CalendarAppointments.Models.Models
{
    public class YearDay
    {
        private Brush color;

        public YearDay()
        {

        }

        public DateTime Date { get; set; }

        public Brush Color
        {
            get => color;
            set { color = value; }
        }
    }
}
