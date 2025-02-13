using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace CritterCare
{
    public partial class CalendarPage : ContentPage
    {
        private DateTime currentWeekStartDate;
        private DatabaseManager _databaseManager;

        public CalendarPage()
        {
            InitializeComponent();
            _databaseManager = new DatabaseManager(); // Initialize database manager
            currentWeekStartDate = GetStartOfWeek(DateTime.Now); // Initialize with the current week
            CreateWeekView(currentWeekStartDate); // Generate the view for the current week
            DisplayAppointments(); // Automatically display appointments
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            var diff = date.DayOfWeek - DayOfWeek.Sunday;
            return date.AddDays(-diff).Date; // Move to the Sunday of the current week
        }

        private void CreateWeekView(DateTime weekStart)
        {
            CurrentWeekLabel.Text = $"{weekStart:MMMM yyyy}";
            WeeklyCalendarLayout.Children.Clear();

            var daysOfWeek = new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            var dayColors = new[]
            {
                Color.FromArgb("#f6a5b2"), // Sunday
                Color.FromArgb("#ffcf99"), // Monday
                Color.FromArgb("#a7d8bb"), // Tuesday
                Color.FromArgb("#90c6ec"), // Wednesday
                Color.FromArgb("#c9a9c9"), // Thursday
                Color.FromArgb("#f8c776"), // Friday
                Color.FromArgb("#9588bb")  // Saturday
            };

            for (int i = 0; i < 7; i++)
            {
                var dayStack = new StackLayout
                {
                    Spacing = 10,
                    Padding = new Thickness(5),
                    BackgroundColor = dayColors[i]
                };

                var dayOfWeekLabel = new Label
                {
                    Text = daysOfWeek[i],
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = 16
                };
                dayStack.Children.Add(dayOfWeekLabel);

                var dayDateLabel = new Label
                {
                    Text = weekStart.AddDays(i).Day.ToString(),
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold
                };
                dayStack.Children.Add(dayDateLabel);

                var appointmentsStack = new StackLayout { Spacing = 5 };
                appointmentsStack.Children.Add(new Label
                {
                    Text = "Appointments",
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Color.FromArgb("#0000FF"),
                    FontSize = 12
                });

                dayStack.Children.Add(appointmentsStack);
                WeeklyCalendarLayout.Children.Add(dayStack);
            }
        }

        // Automatically display appointments when the page is loaded
        private void DisplayAppointments()
        {
            // Fetch all pets and appointments for the current week
            var pets = _databaseManager.GetPets(); // Get all pets
            var appointments = _databaseManager.GetAppointmentsForWeek(currentWeekStartDate);

            // Clear previous appointments from the calendar
            foreach (var child in WeeklyCalendarLayout.Children)
            {
                if (child is StackLayout dayStack)
                {
                    foreach (var item in dayStack.Children)
                    {
                        if (item is StackLayout appointmentsStack)
                        {
                            appointmentsStack.Children.Clear();
                        }
                    }
                }
            }

            // Create a dictionary to easily get pet name by PetId
            var petDictionary = pets.ToDictionary(p => p.Id, p => p.Name);

            // Display the appointments in the corresponding days
            foreach (var appointment in appointments)
            {
                // Get the day of the week for the appointment
                var dayOfWeek = appointment.Date.DayOfWeek;
                var dayStack = WeeklyCalendarLayout.Children[(int)dayOfWeek] as StackLayout;

                // Retrieve the pet name using PetId
                string petName = petDictionary.ContainsKey(appointment.PetId) ? petDictionary[appointment.PetId] : "Unknown Pet";

                var appointmentLabel = new Label
                {
                    Text = $"{petName} - {appointment.Title} at {appointment.FormattedTime}",
                    TextColor = Color.FromArgb("#254E70"),
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    FontSize = 12
                };

                foreach (var item in dayStack.Children)
                {
                    if (item is StackLayout appointmentsStack)
                    {
                        appointmentsStack.Children.Add(appointmentLabel);
                    }
                }
            }
        }

        // Event handler for "Previous" button to go to the previous week
        private void OnPreviousWeekClicked(object sender, EventArgs e)
        {
            currentWeekStartDate = currentWeekStartDate.AddDays(-7);
            CreateWeekView(currentWeekStartDate); // Refresh the view with the new week
            DisplayAppointments(); // Display appointments for the new week
        }

        // Event handler for "Next" button to go to the next week
        private void OnNextWeekClicked(object sender, EventArgs e)
        {
            currentWeekStartDate = currentWeekStartDate.AddDays(7);
            CreateWeekView(currentWeekStartDate); // Refresh the view with the new week
            DisplayAppointments(); // Display appointments for the new week
        }
    }

}
