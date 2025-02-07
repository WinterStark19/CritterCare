using CommunityToolkit.Maui.Views;

namespace CritterCare;

public partial class AddPetPopup : Popup
{
    private DatabaseManager _databaseManager;
    private Action _reloadPetsAction;

    public AddPetPopup(Action reloadPetsAction)
    {
        InitializeComponent();
        _databaseManager = new DatabaseManager();
        _reloadPetsAction = reloadPetsAction;

        var windowWidth = Application.Current.Windows[0].Width;
        var windowHeight = Application.Current.Windows[0].Height;
        this.Size = new Size(windowWidth * 0.65, windowHeight * 0.65);
    }

    private void OnSavePetClicked(object sender, EventArgs e)
    {
        var name = NameEntry.Text;
        var birthDate = BirthDatePicker.Date;
        var species = SpeciesEntry.Text;
        var breed = BreedEntry.Text;
        var weight = double.TryParse(WeightEntry.Text, out double parsedWeight) ? parsedWeight : 0.0;

        var newPet = new Pet
        {
            Name = name,
            BirthDate = birthDate,
            Species = species,
            Breed = breed,
            Weight = weight
        };

        _databaseManager.InsertPet(newPet);
        _reloadPetsAction?.Invoke();
        ClosePopup(sender, e);
    }

    private void OnBirthDateChanged(object sender, DateChangedEventArgs e)
    {
        AgeEntry.Text = CalculateAge(BirthDatePicker.Date).ToString();
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

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}

