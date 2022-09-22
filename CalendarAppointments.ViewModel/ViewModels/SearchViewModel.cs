using System.Collections.ObjectModel;
using CalendarAppointments.Models.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using CalendarAppointments.ViewModel.Extensions;
using System.ComponentModel;

namespace CalendarAppointments.ViewModel.ViewModels
{
    public class SearchViewModel : ObservableObject, INotifyPropertyChanged
    {
        private const string FirstPath = "Appointments.xml";
        private const string SecondPath = "outlook.xml";
        private ObservableCollection<Event> events;
        private ObservableCollection<Event> foundEvents;
        private string title;

        public SearchViewModel()
        {
            events = new ObservableCollection<Event>();
            foundEvents = new ObservableCollection<Event>();
            Events.ReadEventsFromFile(FirstPath);
            Events.ReadEventsFromFile(SecondPath);
        }

        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        public ObservableCollection<Event> FoundEvents
        {
            get => foundEvents;
            set
            {
                foundEvents = value;
                OnPropertyChanged(nameof(FoundEvents));
            }
        }

        public string Title
        {
            get => title;
            set
            {
                FoundEvents.Clear();
                title = value;
                FoundEvents.SearchEvent(Title, Events);
            }
        }
    }
}
