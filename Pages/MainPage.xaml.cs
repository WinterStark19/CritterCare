using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;

namespace CritterCare
{
    public partial class MainPage : ContentPage
    {
        private DatabaseManager _databaseManager;

        public MainPage()
        {
            InitializeComponent();
            _databaseManager = new DatabaseManager();
            LoadPetData(); // Load pets into the UI
        }

        private void OpenAddPetPopup(object sender, EventArgs e)
        {
            this.ShowPopup(new AddPetPopup(LoadPetData));
        }

        public void LoadPetData()
        {
            var pets = _databaseManager.GetPets();
            foreach (var pet in pets)
            {
                pet.ImageSource = ConvertByteArrayToImageSource(pet.Image);
            }
            PetListView.ItemsSource = pets;
        }

        private async void OnPetDetailsClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedPet = button.CommandParameter as Pet;

            if (selectedPet != null)
            {
                await Navigation.PushAsync(new PetDetailsPage(selectedPet)); // Pass the full object
            }
        }

        private async void OnDeletePetClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var petToDelete = (Pet)button.CommandParameter;

            // Create and show the confirmation popup
            var popup = new ConfirmDeletePopup(petToDelete.Name);

            // Subscribe to the event for when the user makes a decision
            popup.DeletionConfirmed += (s, isConfirmed) =>
            {
                if (isConfirmed)
                {
                    // Proceed with deletion
                    _databaseManager.DeletePet(petToDelete.Id);
                    LoadPetData(); // Reload the pet data to update the UI
                }
            };

            // Show the confirmation popup
            await this.ShowPopupAsync(popup);
        }

        private async void OnManageAppointmentsClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedPet = button.CommandParameter as Pet;

            if (selectedPet != null)
            {
                await Navigation.PushAsync(new AppointmentsPage(selectedPet.Id)); // Pass the selected pet ID
            }
        }

        private async void OnManageMedicationsClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedPet = button.CommandParameter as Pet;

            if (selectedPet != null)
            {
                await Navigation.PushAsync(new MedicationsPage(selectedPet.Id)); // Pass the selected pet ID
            }
        }

        private ImageSource ConvertByteArrayToImageSource(byte[] byteArray)
        {
            if (byteArray != null && byteArray.Length > 0)
            {
                return ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));
            }
            return ImageSource.FromFile("default_dog.jpg"); // Fallback image if byte array is null or empty
        }
    }
}
