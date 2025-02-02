using CommunityToolkit.Maui.Views;

namespace CritterCare;

public partial class AddPetPopup : Popup
{
    public AddPetPopup()
    {
        InitializeComponent();
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}