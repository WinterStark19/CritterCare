using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;

namespace CritterCare
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();

            // Create a collection to hold appointments
            var appointments = new List<SchedulerAppointment>
            {
                new SchedulerAppointment
                {
                    StartTime = DateTime.Now.AddHours(1), // Start time of appointment
                    EndTime = DateTime.Now.AddHours(2), // End time of appointment
                    Subject = "Vet Appointment", // Subject of appointment
                    Location = "Happy Pets Clinic" // Location
                },
                new SchedulerAppointment
                {
                    StartTime = DateTime.Now.AddDays(1).AddHours(9),
                    EndTime = DateTime.Now.AddDays(1).AddHours(10),
                    Subject = "Grooming Session",
                    Location = "Local Pet Groomer"
                }
            };

            // Assign appointments collection to the scheduler
            PetScheduler.AppointmentsSource = appointments;
        }
    }
}

