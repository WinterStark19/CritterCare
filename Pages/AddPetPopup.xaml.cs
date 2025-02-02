using CommunityToolkit.Maui.Views;

namespace CritterCare;

public partial class AddPetPopup : Popup
{
    public AddPetPopup()
    {
        InitializeComponent();

        var windowWidth = Application.Current.Windows[0].Width;
        var windowHeight = Application.Current.Windows[0].Height;

        // Set the size of the popup relative to the screen size
        // For example, make the width 30% of the screen width and height 40% of the screen height
        this.Size = new Size(windowWidth * 0.6, windowHeight * 0.6);
    }

    // Handle Save button click to save pet information
    private void OnSavePetClicked(object sender, EventArgs e)
    {
        // Gather the data from the input fields
        var name = NameEntry.Text;
        var birthDate = BirthDatePicker.Date;
        var species = SpeciesEntry.Text;
        var breed = BreedEntry.Text;

        // Create a new pet object
        var newPet = new Pet
        {
            Name = name,
            BirthDate = birthDate,
            Species = species,
            Breed = breed
        };

        // Insert the new pet into the database
        //_databaseManager.InsertPet(newPet);

        // Close the popup after saving
        ClosePopup(sender, e);
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}