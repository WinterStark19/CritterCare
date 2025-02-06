using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace CritterCare
{
    public partial class SettingsPage : ContentPage
    {
        public bool IsDarkMode
        {
            get => Preferences.Get("DarkMode", false);
            set
            {
                Preferences.Set("DarkMode", value);
                ((App)Application.Current).ApplyTheme(); // Notify App to apply theme
            }
        }

        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void OnSaveSettingsClicked(object sender, EventArgs e)
        {
            ((App)Application.Current).ApplyTheme(); // Ensure App level theme is applied
            DisplayAlert("Settings Changed", "Your settings have been saved.", "OK");
        }
    }
}


