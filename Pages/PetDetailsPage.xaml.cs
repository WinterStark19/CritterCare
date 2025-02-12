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
            SelectImageButton.IsVisible = true;
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
            SelectImageButton.IsVisible = false;

            await Navigation.PushAsync(new MainPage()); // Create a new instance of MainPage to reload it
        }

        private async void CancelEdit(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            // Use MediaPicker to allow the user to pick an image from their device
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select a photo"
                });

                if (result != null)
                {
                    // Read the picked image as a stream
                    var imageStream = await result.OpenReadAsync();
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageStream.CopyToAsync(memoryStream);
                        _pet.Image = memoryStream.ToArray();  // Save image as byte array
                        _pet.ImageSource = ImageSource.FromStream(() => new MemoryStream(_pet.Image)); // Convert byte array to ImageSource
                    }

                    // Update the displayed image
                    PetImage.Source = _pet.ImageSource;
                }
            }
            catch (Exception ex)
            {
                // Handle error (e.g., user canceled or error during picking image)
                Console.WriteLine($"Error selecting image: {ex.Message}");
            }
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

