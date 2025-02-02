using CommunityToolkit.Maui.Views;

namespace CritterCare;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        LoadPetData(); // Load pets into the UI

        Routing.RegisterRoute(nameof(AddPetPopup), typeof(AddPetPopup));
    }

    private void OpenAddPetPopup(object sender, EventArgs e)
    {
        this.ShowPopup(new AddPetPopup());
    }

    private void LoadPetData()
    {
        var pets = new List<Pet>
        {
            new Pet { Name = "Lady Pippin Seren" },
            new Pet { Name = "River" },
            new Pet { Name = "Cynda" }
        };

        PetListView.ItemsSource = pets;
    }


    
}
