namespace CritterCare;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        LoadPetData(); // Load pets into the UI
    }

    private void LoadPetData()
    {
        var pets = new List<Pet>
        {
            new Pet { Name = "Pip" },
            new Pet { Name = "River" },
            new Pet { Name = "Cynda" }
        };

        PetListView.ItemsSource = pets;
    }


    
}
