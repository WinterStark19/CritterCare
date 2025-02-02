using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace CritterCare
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Check the dark mode setting in Preferences (default is false)
            bool isDarkMode = Preferences.Get("DarkMode", false);  // Default to light mode

            // If dark mode is enabled, apply the dark theme
            if (isDarkMode)
            {
                // You can use dynamic resources to switch themes (colors, backgrounds, etc.)
                Application.Current.Resources["PrimaryBackgroundColor"] = Color.FromHex("#121212"); // Dark background
                Application.Current.Resources["PrimaryTextColor"] = Color.FromHex("#FFFFFF"); // Dark text
            }
            else
            {
                // Apply light theme
                Application.Current.Resources["PrimaryBackgroundColor"] = Color.FromHex("#FFFFFF"); // Light background
                Application.Current.Resources["PrimaryTextColor"] = Color.FromHex("#000000"); // Light text
            }

            // Return a new window with the AppShell
            return new Window(new AppShell());
        }
    }
}
