using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAppointments.Models.Models
{
    public class Day
    {
        public DateTime Today { get; set; }
        public DateTime Tomorrow { get; set; }
        public int CurrentWeek { get; set; }
        public int TomorrowWeek { get; set; }
        public string Month { get; set; }
        public string TomorrowMonth { get; set; }

        public Day()
        {

        }
    }
}
