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

    private async void GoToProfilePage(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync(nameof(ProfilePage));
    }

    private async void GoToCalendarPage(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync(nameof(CalendarPage));
    }
}
