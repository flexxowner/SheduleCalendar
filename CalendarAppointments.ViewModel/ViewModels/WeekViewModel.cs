using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CalendarAppointments.Models.Models;
using CalendarAppointments.ViewModel.Extensions;
using CalendarAppointments.ViewModel.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class WeekViewModel : ObservableObject, INotifyPropertyChanged
    {
        private const string FirstPath = "Appointments.xml";
        private const string SecondPath = "outlook.xml";
        private readonly ObservableCollection<Week> weeks;
        private readonly ObservableCollection<DateTime> listOfDays;
        private readonly List<DateTime> hours;
        private readonly ObservableCollection<DayHour> dayHours;
        private DateTime currentDate;
        private DateTime thisWeekStart;
        private DateTime thisWeekEnd;

        public WeekViewModel()
        {
            weeks = new ObservableCollection<Week>();
            listOfDays = new ObservableCollection<DateTime>();
            dayHours = new ObservableCollection<DayHour>();
            hours = DataChanger.GetHours();
            CurrentDate = DateTime.Today;
            ThisWeekStart = DataChanger.GetThisWeekStart(currentDate);
            ThisWeekEnd = DataChanger.GetThisWeekEnd(thisWeekStart);
            ListOfDays.AddDaysOfWeek(ThisWeekStart, ThisWeekEnd);
            Weeks.GetWeekList(ListOfDays);
            Weeks.GetWeekDayHours(DayHours, Hours);
            Weeks.AddEvents(FirstPath);
            Weeks.AddEvents(SecondPath);
            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
        }

        public RelayCommand GoBackCommand { get; set; }

        public RelayCommand GoForwardCommand { get; set; }

        public ObservableCollection<Week> Weeks
        {
            get => weeks;
        }

        public ObservableCollection<DateTime> ListOfDays
        {
            get => listOfDays;
        }

        public List<DateTime> Hours
        {
            get => hours;
        }

        public ObservableCollection<DayHour> DayHours
        {
            get => dayHours;
        }

        public DateTime CurrentDate
        {
            get => currentDate;
            set
            {
                currentDate = value;
            }
        }

        public DateTime ThisWeekStart
        {
            get => thisWeekStart;
            set
            {
                thisWeekStart = value;
            }
        }

        public DateTime ThisWeekEnd
        {
            get => thisWeekEnd;
            set
            {
                thisWeekEnd = value;
            }
        }

        private void GoBack()
        {
            Weeks.Clear();
            ListOfDays.Clear();
            CurrentDate = CurrentDate.AddDays(-8);
            ThisWeekStart = DataChanger.GetThisWeekStart(currentDate);
            ThisWeekEnd = DataChanger.GetThisWeekEnd(ThisWeekStart);
            ListOfDays.AddDaysOfWeek(ThisWeekStart, ThisWeekEnd);
            Weeks.GetWeekList(ListOfDays);
            Weeks.GetWeekDayHours(DayHours, Hours);
            Weeks.AddEvents(FirstPath);
            Weeks.AddEvents(SecondPath);
        }

        private void GoForward()
        {
            Weeks.Clear();
            ListOfDays.Clear();
            CurrentDate = CurrentDate.AddDays(8);
            ThisWeekStart = DataChanger.GetThisWeekStart(currentDate);
            ThisWeekEnd = DataChanger.GetThisWeekEnd(thisWeekStart);
            ListOfDays.AddDaysOfWeek(ThisWeekStart, ThisWeekEnd);
            Weeks.GetWeekList(ListOfDays);
            Weeks.GetWeekDayHours(DayHours, Hours);
            Weeks.AddEvents(FirstPath);
            Weeks.AddEvents(SecondPath);
        }

    }
}
