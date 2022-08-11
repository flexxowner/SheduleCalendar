using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.UI.Xaml;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class DayViewModel : ObservableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<string> Hours { get; set; }
        private ObservableCollection<string> firstDayOfWeek;
        private ObservableCollection<DaysOfWeek> daysOfWeeks;
        private DateTimeOffset today = DateTimeOffset.Now;
        private int currentMonth;
        private int currentYear;
        private string currentDayOfWeek;
        private int currentDay;
        private DateTime currentTime;
        private string time;

        public DayViewModel()
        {
            StartClock();
            daysOfWeeks = new ObservableCollection<DaysOfWeek>();
            CurrentMonth = today.Month;
            CurrentYear = today.Year;
            CurrentDayOfWeek = today.DayOfWeek.ToString();
            CurrentDay = today.Day;
            Hours = Enumerable.Range(00, 24).Select(i => (DateTime.MinValue.AddHours(i)).ToString("H:mm"));
        }

        public ObservableCollection<string> FirstDayOfWeek
        {
            get { return firstDayOfWeek; }
            set { firstDayOfWeek = value; }
        }

        public ObservableCollection<DaysOfWeek> DaysOfWeeks
        {
            get { return daysOfWeeks; }
            set { daysOfWeeks = value; }
        }

        public DateTimeOffset Today
        {
            get { return today; }
            set { SetProperty(ref today, value); }
        }

        public int CurrentMonth
        {
            get { return currentMonth; }
            set { currentMonth = value; }
        }

        public int CurrentYear
        {
            get { return currentYear; }
            set { currentYear = value; }
        }

        public string CurrentDayOfWeek
        {
            get { return currentDayOfWeek;}
            set { SetProperty(ref currentDayOfWeek, value); }
        }

        public int CurrentDay
        {
            get { return currentDay;}
            set { currentDay = value; }
        }

        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                this.OnPropertyChanged();

            }
        }

        private void StartClock()
        {
            currentTime = DateTime.Now;
            DispatcherTimer clocktimer = new DispatcherTimer();
            clocktimer.Interval = TimeSpan.FromSeconds(1);
            clocktimer.Tick += Clocktimer_Tick;
            clocktimer.Start();
        }

        private void Clocktimer_Tick(object sender, object e)
        {
            currentTime = DateTime.Now;
            updateTimeDisplay(currentTime);
        }

        private void updateTimeDisplay(DateTime time)
        {
            Time = time.ToString(@"HH:mm:ss");
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
