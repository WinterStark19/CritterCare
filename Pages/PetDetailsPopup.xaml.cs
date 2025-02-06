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
            PetNameEntry.Text = _pet.Name;
            PetSpeciesEntry.Text = _pet.Species;
            PetBreedEntry.Text = _pet.Breed;
            PetBirthDatePicker.Date = _pet.BirthDate;
            PetWeightEntry.Text = _pet.Weight.ToString() + " lbs";

            // Set the initial size based on the window dimensions
            var windowWidth = Application.Current.Windows[0].Width;
            var windowHeight = Application.Current.Windows[0].Height;
            this.Size = new Size(windowWidth * 0.65, windowHeight * 0.65);
        }

        // Close the popup
        private void ClosePopup(object sender, EventArgs e)
        {
            Close();
        }

        // Handle edit action
        private void EditPet(object sender, EventArgs e)
        {
            // Enable editing of fields
            PetNameEntry.IsReadOnly = false;
            PetSpeciesEntry.IsReadOnly = false;
            PetBreedEntry.IsReadOnly = false;
            PetWeightEntry.IsReadOnly = false;
            PetBirthDatePicker.IsEnabled = true;
        }
    }
}
