using CommunityToolkit.Maui.Views;

namespace CritterCare;

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

        // Bind the fetched data to the CollectionView
        PetListView.ItemsSource = pets;
    }

    private async void OnPetSelected(object sender, SelectionChangedEventArgs e)
    {
        // Ensure that a pet is selected
        if (e.CurrentSelection.Count > 0)
        {
            var selectedPet = e.CurrentSelection[0] as Pet;
            if (selectedPet != null)
            {
                // Show the PetDetailsPopup with the selected pet's details
                var petDetailsPopup = new PetDetailsPopup(selectedPet, LoadPetData);
                await this.ShowPopupAsync(petDetailsPopup);
            }
        }
    }

}
