using Microsoft.Maui.Controls;
using System;
using CommunityToolkit.Maui.Views;

namespace CritterCare
{
    public partial class PetDetailsPage : ContentPage
    {
        private readonly Pet _pet;
        private readonly DatabaseManager _databaseManager;

        public PetDetailsPage(Pet pet)
        {
            InitializeComponent();
            _pet = pet;
            _databaseManager = new DatabaseManager();

            // Populate UI fields with pet details
            PetNameEntry.Text = _pet.Name;
            PetSpeciesEntry.Text = _pet.Species;
            PetBreedEntry.Text = _pet.Breed;
            PetBirthDatePicker.Date = _pet.BirthDate;
            PetWeightEntry.Text = _pet.Weight.ToString();
            AgeEntry.Text = CalculateAge(_pet.BirthDate).ToString();
        }

        private void EditPet(object sender, EventArgs e)
        {
            // Enable editing
            PetNameEntry.IsReadOnly = false;
            PetSpeciesEntry.IsReadOnly = false;
            PetBreedEntry.IsReadOnly = false;
            PetWeightEntry.IsReadOnly = false;
            PetBirthDatePicker.IsEnabled = true;

            AgeEntry.IsReadOnly = true; // Age should always remain read-only
            AgeCard.IsVisible = false;

            SaveButton.IsVisible = true; // Show Save button
        }

        private async void SavePet(object sender, EventArgs e)
        {
            _pet.Name = PetNameEntry.Text;
            _pet.Species = PetSpeciesEntry.Text;
            _pet.Breed = PetBreedEntry.Text;
            _pet.BirthDate = PetBirthDatePicker.Date;
            _pet.Weight = double.TryParse(PetWeightEntry.Text, out double parsedWeight) ? parsedWeight : _pet.Weight;

            _databaseManager.UpdatePet(_pet);

            // Disable editing after saving
            PetNameEntry.IsReadOnly = true;
            PetSpeciesEntry.IsReadOnly = true;
            PetBreedEntry.IsReadOnly = true;
            PetWeightEntry.IsReadOnly = true;
            PetBirthDatePicker.IsEnabled = false;

            AgeEntry.IsReadOnly = true;
            AgeCard.IsVisible = true;

            SaveButton.IsVisible = false;

            // Navigate back to the previous page
            await Navigation.PopAsync();
        }

        private async void CancelEdit(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now < birthDate.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}

