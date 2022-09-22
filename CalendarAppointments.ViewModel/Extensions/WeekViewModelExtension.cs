using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace CalendarAppointments.ViewModel.Extensions
{
    public static class WeekViewModelExtension
    {
        public static void AddDaysOfWeek(
            this ObservableCollection<DateTime> days, DateTime startDate, DateTime endDate)
        {
            DateFiller.AddDaysOfWeek(days, startDate, endDate);
        }

        public static void GetWeekList(this ObservableCollection<Week> weeks, ObservableCollection<DateTime> listOfDays)
        {
            DateFiller.AddDateToWeekList(weeks, listOfDays);
        }

        public static void GetWeekDayHours(this ObservableCollection<Week> weeks, ObservableCollection<DayHour> dayHours, List<DateTime> hours)
        {
            DateFiller.AddHoursToWeekDays(weeks, dayHours, hours);
        }

        public static void AddEvents(this ObservableCollection<Week> weeks, string path)
        {
            foreach (var item in weeks)
            {
                EventService.ReadEventsFromFile(item.Hours, path);
            }
        }
    }
}
