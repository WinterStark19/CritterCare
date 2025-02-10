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

        private void LoadPetData()
        {
            var pets = _databaseManager.GetPets();
            foreach (var pet in pets)
            {
                pet.ImageSource = ConvertByteArrayToImageSource(pet.Image);
            }
            PetListView.ItemsSource = pets;
        }

        private async void OnPetSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                var selectedPet = e.CurrentSelection[0] as Pet;
                if (selectedPet != null)
                {
                    var petDetailsPopup = new PetDetailsPopup(selectedPet, LoadPetData);
                    await this.ShowPopupAsync(petDetailsPopup);
                    PetListView.SelectedItem = null; // Clear selection
                }
            }
        }

        // Method to handle the delete button click
        private void OnDeletePetClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var petToDelete = (Pet)button.CommandParameter;
            _databaseManager.DeletePet(petToDelete.Id);
            LoadPetData();
        }

        private ImageSource ConvertByteArrayToImageSource(byte[] byteArray)
        {
            if (byteArray != null && byteArray.Length > 0)
            {
                return ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));
            }
            return ImageSource.FromFile("default_dog.jpg"); // Fallback image if byte array is null or empty
        }

        // Navigate to AppointmentsPage
        private async void OnManageAppointmentsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppointmentsPage());
        }

        // Navigate to MedicationsPage
        private async void OnManageMedicationsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MedicationsPage());
        }
    }
}

