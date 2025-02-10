﻿using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Linq;

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
            // Fetch pets from the database
            var pets = _databaseManager.GetPets();

            // Convert the byte array for each pet image into ImageSource
            foreach (var pet in pets)
            {
                pet.ImageSource = ConvertByteArrayToImageSource(pet.Image);
            }

            // Bind the fetched data to the CollectionView
            PetListView.ItemsSource = pets;
        }

        //private async void OnPetSelected(object sender, SelectionChangedEventArgs e)
        //{
        //    // Ensure that a pet is selected
        //    if (e.CurrentSelection.Count > 0)
        //    {
        //        var selectedPet = e.CurrentSelection[0] as Pet;
        //        if (selectedPet != null)
        //        {
        //            // Show the PetDetailsPopup with the selected pet's details
        //            var petDetailsPopup = new PetDetailsPopup(selectedPet, LoadPetData);
        //            await this.ShowPopupAsync(petDetailsPopup);
        //        }
        //    }
        //}
        private async void OnPetSelected(object sender, SelectionChangedEventArgs e)
        {
            // pet is selected
            if (e.CurrentSelection.Count > 0)
            {
                var selectedPet = e.CurrentSelection[0] as Pet;
                if (selectedPet != null)
                {
                   
                    await Navigation.PushAsync(new PetDetailsPage(selectedPet));
                }
            }
        }

        // Method to convert byte[] to ImageSource
        private ImageSource ConvertByteArrayToImageSource(byte[] byteArray)
        {
            if (byteArray != null && byteArray.Length > 0)
            {
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
            return ImageSource.FromFile("default_dog.jpg"); // Fallback image if byte array is null or empty
        }
    }
}
