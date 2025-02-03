using CommunityToolkit.Maui.Views;

namespace CritterCare;

/// <summary>
/// Represents a popup for adding a new pet in the CritterCare application.
/// It allows the user to input pet information and save it to the database.
/// </summary>
public partial class AddPetPopup : Popup
{

    // Initialize the database and reload action
    private DatabaseManager _databaseManager;
    private Action _reloadPetsAction;

    /// <summary>
    /// Initializes a new instance of the AddPetPopup class.
    /// Sets up the popup's size relative to the screen size and stores the action to reload the pet list
    /// on the main page after a new pet is added.
    /// </summary>
    /// <param name="reloadPetsAction">Action to reload the pet list after saving a new pet.</param>
    public AddPetPopup(Action reloadPetsAction)
    {
        InitializeComponent();
        _databaseManager = new DatabaseManager();
        _reloadPetsAction = reloadPetsAction;

        var windowWidth = Application.Current.Windows[0].Width;
        var windowHeight = Application.Current.Windows[0].Height;

        // Set the size of the popup relative to the screen size
        // For example, make the width 30% of the screen width and height 40% of the screen height
        this.Size = new Size(windowWidth * 0.6, windowHeight * 0.6);
    }

    /// <summary>
    /// Handles the Save button click event. Gathers data from input fields, creates a new pet object, 
    /// inserts it into the database, and triggers the reload action which reloads the list on the main page.
    /// </summary>
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
        _databaseManager.InsertPet(newPet);

        // Reload the pet list in the main page to show the newly added pet
        _reloadPetsAction?.Invoke();

        // Close the popup after saving
        ClosePopup(sender, e);
    }

    /// <summary>
    /// Closes the popup window.
    /// </summary>
    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}