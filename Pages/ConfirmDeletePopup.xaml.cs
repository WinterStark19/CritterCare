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
