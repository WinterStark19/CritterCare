using CommunityToolkit.Maui.Views;

namespace CritterCare
{
    public partial class PetDetailsPopup : ContentPage
    {
        private readonly Pet _pet;
        private readonly Action _reloadDataCallback;
        private readonly DatabaseManager _databaseManager; // Added database manager

        public PetDetailsPopup(Pet pet, Action reloadDataCallback)
        {
            InitializeComponent();
            _pet = pet;
            _reloadDataCallback = reloadDataCallback;
            _databaseManager = new DatabaseManager(); // Initialize it

            // Set the pet details in the popup fields
            PetNameEntry.Text = _pet.Name;
            PetSpeciesEntry.Text = _pet.Species;
            PetBreedEntry.Text = _pet.Breed;
            PetBirthDatePicker.Date = _pet.BirthDate;
            PetWeightEntry.Text = _pet.Weight.ToString();
            AgeEntry.Text = CalculateAge(_pet.BirthDate).ToString();

            // Set initial size based on the window dimensions
            var windowWidth = Application.Current.Windows[0].Width;
            var windowHeight = Application.Current.Windows[0].Height;
        }

        private void EditPet(object sender, EventArgs e)
        {
            // Enable editing of specific fields
            PetNameEntry.IsReadOnly = false;
            PetSpeciesEntry.IsReadOnly = false;
            PetBreedEntry.IsReadOnly = false;
            PetWeightEntry.IsReadOnly = false;
            PetBirthDatePicker.IsEnabled = true;

            // Ensure Age remains read-only
            AgeEntry.IsReadOnly = true;
            AgeCard.IsVisible = false;

            SaveButton.IsVisible = true; // Show the save button when editing
        }

        private void SavePet(object sender, EventArgs e)
        {
            _pet.Name = PetNameEntry.Text;
            _pet.Species = PetSpeciesEntry.Text;
            _pet.Breed = PetBreedEntry.Text;
            _pet.BirthDate = PetBirthDatePicker.Date;
            _pet.Weight = double.TryParse(PetWeightEntry.Text, out double parsedWeight) ? parsedWeight : _pet.Weight;

            _databaseManager.UpdatePet(_pet);
            _reloadDataCallback?.Invoke();
        }

        private void ClosePopup(object sender, EventArgs e)
        {
            // Enable editing of specific fields
            PetNameEntry.IsReadOnly = true;
            PetSpeciesEntry.IsReadOnly = true;
            PetBreedEntry.IsReadOnly = true;
            PetWeightEntry.IsReadOnly = true;
            PetBirthDatePicker.IsEnabled = false;

            // Ensure Age remains read-only
            AgeEntry.IsReadOnly = false;
            AgeCard.IsVisible = true;

            SaveButton.IsVisible = false; // Show the save button when editing
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
