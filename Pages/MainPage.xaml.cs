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

        private async void OnPetDetailsClicked(object sender, EventArgs e)
        {
            // Get the "Details" button that was clicked
            var button = (Button)sender;

            // Get the selected pet from the CommandParameter (bound to the pet object)
            var selectedPet = button.CommandParameter as Pet;

            // Ensure that the pet object is valid
            if (selectedPet != null)
            {
                // Show the PetDetailsPopup with the selected pet's details
                var petDetailsPopup = new PetDetailsPopup(selectedPet, LoadPetData);
                await this.ShowPopupAsync(petDetailsPopup);
            }
        }
        //private async void OnPetSelected(object sender, SelectionChangedEventArgs e)
        //{
        //    // pet is selected
        //    if (e.CurrentSelection.Count > 0)
        //    {
        //        var selectedPet = e.CurrentSelection[0] as Pet;
        //        if (selectedPet != null)
        //        {

        // Method to handle the delete button click
        private async void OnDeletePetClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var petToDelete = (Pet)button.CommandParameter;

            // Call the method in DatabaseManager to delete the pet
            _databaseManager.DeletePet(petToDelete.Id);  // Assuming `Id` is the primary key or unique identifier for a pet

            // Reload the pet data to update the UI
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

