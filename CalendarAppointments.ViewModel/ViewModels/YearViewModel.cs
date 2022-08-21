using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class YearViewModel : ObservableObject
    {
        private ObservableCollection<Year> years;
        private DateTime today;
        private List<DateTime> dates;

        public YearViewModel()
        {
            today = DateTime.Now;
            years = new ObservableCollection<Year>();
            dates = new List<DateTime>();
        }

        private void AddMonths()
        {
            for (int i = 1; i < 12; i++)
            {
                dates.Add(new DateTime(today.Year, i,1));
            }
        }
        private void AddYears()
        {
            for (int i = 0; i < dates.Count; i++)
            {
                years.Add(new Year() { CurrentYear = dates[i].Year, Day = DateTime.DaysInMonth(dates[i].Year, dates[i].Month), Month = dates[i].Month, });
            }
        }
    }
}
