using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Service;
using Helpers.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using DayOfWeek = CalendarAppointments.Models.Models.DayOfWeek;

namespace CalendarAppointments.ViewModel.Extensions
{
    public static class MonthViewModelExtension
    {
        public static void AddDaysOfMonth(
            this ObservableCollection<MonthDay> calendarDays, int CurrentYear, int CurrentMonth)
        {
            DateFiller.AddDaysOfMonth(calendarDays, CurrentYear, CurrentMonth);
        }

        public static void UpdateMonths(
            this ObservableCollection<CalendarMonth> months, DateTimeOffset today, string format, string culture)
        {
            DateFiller.AddMonths(months, today, format, culture);
        }

        public static void AddDaysOfWeek(
            this ObservableCollection<DayOfWeek> daysOfWeeks, int CurrentYear, int CurrentMonth)
        {
            DayOfWeekService.GetListOfWeekDays(CurrentYear, CurrentMonth);
            DayOfWeekService.AddDaysOfWeek(daysOfWeeks, CurrentYear, CurrentMonth );
        }

        public static void SaveToSelectedDay(
            this ObservableCollection<Event> events, MonthDay SelectedDay, ObservableCollection<MonthDay> calendarDays )
        {
            EventService.SaveToSelectedDay(events, SelectedDay, calendarDays);
        }
    }
}
