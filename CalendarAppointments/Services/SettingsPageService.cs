using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CalendarAppointments.Services
{
    public class SettingsPageService : ObservableObject
    {
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;
        private ICommand _switchThemeCommand;

        public SettingsPageService()
        {
        }

        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { SetProperty(ref _elementTheme, value); }
        }

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }
    }
}
