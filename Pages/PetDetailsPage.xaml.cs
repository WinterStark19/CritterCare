using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace CritterCare
{
    public partial class PetDetailsPage : ContentPage
    {
        private DatabaseManager _databaseManager;
        private Pet _pet;
        private bool _isEditing = false;

        public PetDetailsPage(Pet pet)
        {
            InitializeComponent();
            _databaseManager = new DatabaseManager();
            _pet = pet;
            LoadPetDetails();
            LoadAppointments();
            LoadMedications();
        }

        private void LoadPetDetails()
        {
            PetNameEntry.Text = _pet.Name;
            PetSpeciesEntry.Text = _pet.Species;
            PetBreedEntry.Text = _pet.Breed;
            PetBirthDatePicker.Date = _pet.BirthDate;
            PetWeightEntry.Text = _pet.Weight.ToString();
            PetAgeEntry.Text = CalculateAge(_pet.BirthDate).ToString();
            PetAgeEntry.IsReadOnly = true; // Ensure Age is read-only
            ToggleEditMode(false);
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        private void LoadAppointments()
        {
            var appointments = _databaseManager.GetAppointmentsForPet(_pet.Id);
            AppointmentListView.ItemsSource = appointments;
            NoAppointmentsLabel.IsVisible = appointments.Count == 0; // Show the "No appointments" message if the list is empty
        }

        private void LoadMedications()
        {
            var medications = _databaseManager.GetMedicationsForPet(_pet.Id);
            MedicationListView.ItemsSource = medications;
            NoMedicationsLabel.IsVisible = medications.Count == 0; // Show the "No medications" message if the list is empty
        }

        private void ToggleEditMode(bool enable)
        {
            _isEditing = enable;
            PetNameEntry.IsReadOnly = !enable;
            PetSpeciesEntry.IsReadOnly = !enable;
            PetBreedEntry.IsReadOnly = !enable;
            PetWeightEntry.IsReadOnly = !enable;
            EditButton.IsVisible = !enable;
            SaveButton.IsVisible = enable;
        }

        private void OnEditClicked(object sender, EventArgs e)
        {
            ToggleEditMode(true);
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            _pet.Name = PetNameEntry.Text;
            _pet.Species = PetSpeciesEntry.Text;
            _pet.Breed = PetBreedEntry.Text;
            _pet.Weight = double.TryParse(PetWeightEntry.Text, out double weight) ? weight : _pet.Weight;

            _databaseManager.UpdatePet(_pet);
            PetAgeEntry.Text = CalculateAge(_pet.BirthDate).ToString(); // Update age on save
            ToggleEditMode(false);
        }

        private void OnBackToHomeClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        //private void OnAddAppointmentClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new AddAppointmentPage(_pet.Id, LoadAppointments));
        //}

        //private void OnAddMedicationClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new AddMedicationPage(_pet.Id, LoadMedications));
        //}
    }
}
