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

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }
}