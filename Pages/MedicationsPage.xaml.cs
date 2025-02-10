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
    }
}
