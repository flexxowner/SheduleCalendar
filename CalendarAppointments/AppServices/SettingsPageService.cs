using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;
using Windows.UI.Xaml;

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
            get => _elementTheme;

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
