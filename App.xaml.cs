using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace CritterCare
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ApplyTheme(); // Ensure theme is applied at startup
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        public void ApplyTheme()
        {
            bool isDarkMode = Preferences.Get("DarkMode", false);

            if (isDarkMode)
            {
                Current.Resources["PageBackgroundColor"] = Current.Resources["PageBackgroundColorDark"];
                Current.Resources["TextColor"] = Current.Resources["TextColorDark"];
                Current.Resources["FlyoutTextColor"] = Current.Resources["FlyoutTextColorDark"];
            }
            else
            {
                Current.Resources["PageBackgroundColor"] = Current.Resources["PageBackgroundColor"];
                Current.Resources["TextColor"] = Current.Resources["TextColor"];
                Current.Resources["FlyoutTextColor"] = Current.Resources["FlyoutTextColor"];
            }

            // Force the application to refresh the resources
            foreach (var resource in Current.Resources.Keys)
            {
                var colorResource = Current.Resources[resource] as Color;
                if (colorResource != null)
                {
                    Current.Resources[resource] = colorResource;
                }
            }
        }
    }
}

