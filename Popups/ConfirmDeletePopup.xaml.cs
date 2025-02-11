using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;

namespace CritterCare
{
    public partial class ConfirmDeletePopup : Popup
    {
        public event EventHandler<bool> DeletionConfirmed;

        public ConfirmDeletePopup(string petName)
        {
            InitializeComponent();

            // Set initial size based on the window dimensions
            var windowWidth = Application.Current.Windows[0].Width;
            var windowHeight = Application.Current.Windows[0].Height;
            this.Size = new Size(windowWidth * 0.4, windowHeight * 0.4);
        }

        private void OnYesClicked(object sender, EventArgs e)
        {
            // Notify that the deletion is confirmed
            DeletionConfirmed?.Invoke(this, true);
            Close();
        }

        private void OnNoClicked(object sender, EventArgs e)
        {
            // Notify that the deletion is canceled
            DeletionConfirmed?.Invoke(this, false);
            Close();
        }
    }
}
