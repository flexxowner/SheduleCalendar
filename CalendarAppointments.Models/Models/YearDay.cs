using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace CalendarAppointments.Models.Models
{
    public class YearDay
    {
        public DateTime Date { get; set; }
        private Brush color;

        public YearDay()
        {

        }

        public Brush Color
        {
            get { return color; }
            set { color = value; }
        }
    }
}
