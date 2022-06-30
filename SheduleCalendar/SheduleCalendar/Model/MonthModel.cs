using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace SheduleCalendar.Model
{
    public class MonthModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime Date { get; set; }

        // Gets the text label
        public string Label
        {
            get
            {
                return Date.Day.ToString();
            }
        }

        //Gets or sets the column index.
        public int ColumnIndex { get; set; }

        // Gets or sets the row index.
        public int RowIndex { get; set; }

        // Gets or sets whether the day is not in current month (is in another view).
        public bool IsInOtherView { get; set; }


        Thickness _margin = new Thickness(0);

        /// Gets or sets margin.
        public Thickness Margin
        {
            get
            {
                return _margin;
            }
            set
            {
                _margin = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
