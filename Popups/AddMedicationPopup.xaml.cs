﻿using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;
using System;

namespace CritterCare
{
    public partial class AddMedicationPopup : Popup
    {
        private DatabaseManager _databaseManager;
        private int _petId;
        private Action _reloadMedications;

        public AddMedicationPopup(int petId, Action reloadMedications)
        {
            InitializeComponent();
            _databaseManager = new DatabaseManager();
            _petId = petId;
            _reloadMedications = reloadMedications;

            // Set initial size based on the window dimensions
            var windowWidth = Application.Current.Windows[0].Width;
            var windowHeight = Application.Current.Windows[0].Height;
            this.Size = new Size(windowWidth * 0.5, windowHeight * 0.5);
        }

        private void OnSaveMedicationClicked(object sender, EventArgs e)
        {
            var newMedication = new Medication
            {
                PetId = _petId,
                Name = NameEntry.Text,
                Dosage = DosageEntry.Text,
                Frequency = FrequencyEntry.Text
            };

            // Save to the database
            _databaseManager.InsertMedication(newMedication);

            // Reload medications on the main page
            _reloadMedications();

            // Close the popup
            Close();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            // Close the popup without saving
            Close();
        }
    }
}
