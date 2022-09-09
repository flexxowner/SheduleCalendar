using System.Collections.ObjectModel;

namespace CalendarAppointments.Models.Models
{
    public class Year
    {
        public ObservableCollection<string> DaysOfWeek { get; set; }

        public ObservableCollection<YearDay> CalendarDays { get; set; }

        public string Month { get; set; }
    }
}
