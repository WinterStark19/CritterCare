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

        Routing.RegisterRoute(nameof(AddPetPopup), typeof(AddPetPopup));
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



}
