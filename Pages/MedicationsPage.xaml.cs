using CommunityToolkit.Maui.Views;
using CritterCare;
using Microsoft.Maui.Controls;
using System;

namespace CritterCare
{
    public partial class MedicationsPage : ContentPage
    {
        private DatabaseManager _databaseManager;
        private int _petId;

        public MedicationsPage(int petId)
        {
            InitializeComponent();
            _databaseManager = new DatabaseManager();
            _petId = petId;
            LoadMedications();
        }

        private void LoadMedications()
        {
            var medications = _databaseManager.GetMedicationsForPet(_petId);
            MedicationListView.ItemsSource = medications;
        }

        private void OnAddMedicationClicked(object sender, EventArgs e)
        {
            // Show popup to add a new medication
            this.ShowPopup(new AddMedicationPopup(_petId, LoadMedications));
        }
        private async void OnEditMedicationClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var medicationToEdit = (Medication)button.CommandParameter;

            // Show popup to edit the medication
            var editPopup = new EditMedicationPopup(medicationToEdit, LoadMedications);
            await this.ShowPopupAsync(editPopup);
        }

        //private async void OnDeleteMedicationClicked(object sender, EventArgs e)
        //{
        //    var button = (Button)sender;
        //    var medicationToDelete = (Medication)button.CommandParameter;

        //    // Create and show the confirmation popup
        //    var popup = new ConfirmDeletePopup(medicationToDelete.Name);

        //    popup.DeletionConfirmed += (s, isConfirmed) =>
        //    {
        //        if (isConfirmed)
        //        {
        //            _databaseManager.DeleteMedication(medicationToDelete.Id);
        //            LoadMedications();
        //        }
        //    };

        //    // Show the confirmation popup
        //    await this.ShowPopupAsync(popup);
        //}
        private async void OnDeleteMedicationClicked(object sender, EventArgs e)
        {
            var frame = (Frame)sender;
            var medicationToDelete = (Medication)frame.BindingContext;

            // Create and show the confirmation popup
            var popup = new ConfirmDeletePopup(medicationToDelete.Name);

            popup.DeletionConfirmed += (s, isConfirmed) =>
            {
                if (isConfirmed)
                {
                    _databaseManager.DeleteMedication(medicationToDelete.Id);
                    LoadMedications();
                }
            };

            // Show the confirmation popup
            await this.ShowPopupAsync(popup);
        }


    }
}
