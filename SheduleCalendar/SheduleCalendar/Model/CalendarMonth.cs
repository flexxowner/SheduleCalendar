using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheduleCalendar.Model
{
    public class CalendarMonth
    {
        public List<Event> List { get; set; }

        public int Year { get; }

        public int Month { get; }

        public List<CalendarDay> Days { get; set; }

        public CalendarMonth(DateTime date)
        {
            Year = date.Year;
            Month = date.Month;
            List = new List<Event>();
            Days = new List<CalendarDay>
            {
                new CalendarDay(date)
            };
        }
    }
}
