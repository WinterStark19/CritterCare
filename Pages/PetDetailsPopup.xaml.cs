using CommunityToolkit.Maui.Views;

namespace CritterCare
{
    public partial class PetDetailsPopup : Popup
    {
        private readonly Pet _pet;
        private readonly Action _reloadDataCallback;

        public PetDetailsPopup(Pet pet, Action reloadDataCallback)
        {
            InitializeComponent();
            _pet = pet;
            _reloadDataCallback = reloadDataCallback;

            // Set the pet details in the popup fields
            PetNameLabel.Text = _pet.Name;
            PetSpeciesLabel.Text = _pet.Species;
        }

        // Close the popup
        private void ClosePopup(object sender, EventArgs e)
        {
            Close();
        }

        // Handle edit action (just a simple example)
        private void EditPet(object sender, EventArgs e)
        {
            // Handle the logic for editing the pet details
            // You could open a new page or modify the popup to allow editing
            _reloadDataCallback.Invoke(); // Refresh data after edit
            Close();
        }
    }
}

