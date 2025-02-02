using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace CritterCare
{
    public partial class SettingsPage : ContentPage
    {
        // Default to false (light mode)
        public bool IsDarkMode
        {
            get => Preferences.Get("DarkMode", false);  // Default is false
            set => Preferences.Set("DarkMode", value);   // Save setting
        }

        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = this; 
        }

        // Handle the save button click event
        private void OnSaveSettingsClicked(object sender, EventArgs e)
        {
            // Dark mode is automatically saved by the property setter
            DisplayAlert("Settings Changed", "Your settings have been saved.", "OK");
        }
    }
}
