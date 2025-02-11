﻿using CommunityToolkit.Maui.Views;
using CritterCare;
using Microsoft.Maui.Controls;
using System;

namespace CritterCare
{
    public partial class AppointmentsPage : ContentPage
    {
        private DatabaseManager _databaseManager;
        private int _petId;

        public AppointmentsPage(int petId)
        {
            InitializeComponent();
            _databaseManager = new DatabaseManager();
            _petId = petId;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            var appointments = _databaseManager.GetAppointmentsForPet(_petId);
            AppointmentListView.ItemsSource = appointments;
        }

        private void OnAddAppointmentClicked(object sender, EventArgs e)
        {
            // Show popup to add a new appointment
            this.ShowPopup(new AddAppointmentPopup(_petId, LoadAppointments));
        }

        private async void OnDeleteAppointmentClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var appointmentToDelete = (Appointment)button.CommandParameter;

            // Create and show the confirmation popup
            var popup = new ConfirmDeletePopup(appointmentToDelete.Title);

            popup.DeletionConfirmed += (s, isConfirmed) =>
            {
                if (isConfirmed)
                {
                    _databaseManager.DeleteAppointment(appointmentToDelete.Id);
                    LoadAppointments(); // Reload the appointment data to update the UI
                }
            };

            // Show the confirmation popup
            await this.ShowPopupAsync(popup);
        }
    }
}


