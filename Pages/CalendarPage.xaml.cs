using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace CritterCare
{
    public partial class CalendarPage : ContentPage
    {
        private DateTime currentWeekStartDate;

        public CalendarPage()
        {
            InitializeComponent();
            currentWeekStartDate = GetStartOfWeek(DateTime.Now); // Initialize with the current week
            CreateWeekView(currentWeekStartDate); // Generate the view for the current week
        }

        // Method to get the start date of the week for a given date
        private DateTime GetStartOfWeek(DateTime date)
        {
            var diff = date.DayOfWeek - DayOfWeek.Sunday;
            return date.AddDays(-diff).Date; // Move to the Sunday of the current week
        }

        // Method to create the view for the current week
        private void CreateWeekView(DateTime weekStart)
        {
            CurrentWeekLabel.Text = $"{weekStart:MMMM d, yyyy} - {weekStart.AddDays(6):MMMM d, yyyy}";
            WeeklyCalendarLayout.Children.Clear();

            var daysOfWeek = new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            var dayColors = new[]
            {
                Color.FromArgb("#F8BCC6"), // Sunday
                Color.FromArgb("#FFDBB2"), // Monday
                Color.FromArgb("#B9E0C9"), // Tuesday
                Color.FromArgb("#A6D1F0"), // Wednesday
                Color.FromArgb("#D3B9D3"), // Thursday
                Color.FromArgb("#F9D18E"), // Friday
                Color.FromArgb("#A398C4")
            };

            // Loop through each day in the week and create its layout
            for (int i = 0; i < 7; i++)
            {
                // Create a stack for each day
                var dayStack = new StackLayout
                {
                    Spacing = 10,
                    Padding = new Thickness(5),
                    BackgroundColor = dayColors[i] // Set unique color for each day
                };

                // Add a label for the day of the week
                var dayOfWeekLabel = new Label
                {
                    Text = daysOfWeek[i],
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = 16
                };
                dayStack.Children.Add(dayOfWeekLabel);

                // Add a label for the date of the day
                var dayDateLabel = new Label
                {
                    Text = weekStart.AddDays(i).Day.ToString(),
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold
                };
                dayStack.Children.Add(dayDateLabel);

                // Stack for appointments under the day
                var appointmentsStack = new StackLayout { Spacing = 5 };
                appointmentsStack.Children.Add(new Label
                {
                    Text = "Appointments",
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Color.FromArgb("#0000FF"),
                    FontSize = 12
                });

                // Optionally, appointments will be dynamically added here later
                dayStack.Children.Add(appointmentsStack);

                // Add the entire day stack to the week layout
                WeeklyCalendarLayout.Children.Add(dayStack);
            }
        }

        // Event handler for "Previous" button to go to the previous week
        private void OnPreviousWeekClicked(object sender, EventArgs e)
        {
            currentWeekStartDate = currentWeekStartDate.AddDays(-7);
            CreateWeekView(currentWeekStartDate);
        }

        // Event handler for "Next" button to go to the next week
        private void OnNextWeekClicked(object sender, EventArgs e)
        {
            currentWeekStartDate = currentWeekStartDate.AddDays(7);
            CreateWeekView(currentWeekStartDate);
        }

        // Event handler for showing appointments
        private void OnShowAppointmentsClicked(object sender, EventArgs e)
        {
            // Example appointments for the week
            var appointments = new List<SchedulerAppointment>
            {
                new SchedulerAppointment
                {
                    StartTime = DateTime.Now.AddHours(1),
                    EndTime = DateTime.Now.AddHours(2),
                    Subject = "Vet Appointment",
                    Location = "Happy Pets Clinic"
                },
                new SchedulerAppointment
                {
                    StartTime = DateTime.Now.AddDays(1).AddHours(9),
                    EndTime = DateTime.Now.AddDays(1).AddHours(10),
                    Subject = "Grooming Session",
                    Location = "Local Pet Groomer"
                }
            };

            DisplayAppointments(appointments);
        }

        // Display appointments for the selected week
        private void DisplayAppointments(List<SchedulerAppointment> appointments)
        {
            // Clear previous appointments
            foreach (var child in WeeklyCalendarLayout.Children)
            {
                if (child is StackLayout dayStack)
                {
                    // Loop through each day in the stack and find the appointments section
                    foreach (var item in dayStack.Children)
                    {
                        if (item is StackLayout appointmentsStack)
                        {
                            appointmentsStack.Children.Clear();
                        }
                    }
                }
            }

            // Display the appointments in the corresponding days
            foreach (var appointment in appointments)
            {
                // Get the day of the week for the appointment
                var dayOfWeek = appointment.StartTime.DayOfWeek;
                var dayStack = WeeklyCalendarLayout.Children[(int)dayOfWeek] as StackLayout;

                var appointmentLabel = new Label
                {
                    Text = $"{appointment.Subject} - {appointment.StartTime:hh:mm tt}",
                    TextColor = Color.FromArgb("#0000FF"),
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    FontSize = 12
                };

                // Find the StackLayout where appointments should go
                foreach (var item in dayStack.Children)
                {
                    if (item is StackLayout appointmentsStack)
                    {
                        appointmentsStack.Children.Add(appointmentLabel);
                    }
                }
            }
        }
    }

    // Appointment model
    public class SchedulerAppointment
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
    }
}