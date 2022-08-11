using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAppointments.Models.Models
{
    public class Event 
    {
        public string Title { get; set; }
        public DateTimeOffset StartTime { get; set; }
    }
}
