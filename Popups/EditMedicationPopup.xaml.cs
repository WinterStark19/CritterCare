using CommunityToolkit.Maui.Views;
using System;

namespace CritterCare
{
    public partial class EditMedicationPopup : Popup
    {
        private readonly Medication _medication;
        private readonly Action _reloadMedicationsCallback;
        private readonly DatabaseManager _databaseManager;

        public EditMedicationPopup(Medication medication, Action reloadMedicationsCallback)
        {
            InitializeComponent();
            _medication = medication;
            _reloadMedicationsCallback = reloadMedicationsCallback;
            _databaseManager = new DatabaseManager(); // Initialize it

            // Set initial size based on the window dimensions
            var windowWidth = Application.Current.Windows[0].Width;
            var windowHeight = Application.Current.Windows[0].Height;
            this.Size = new Size(windowWidth * 0.5, windowHeight * 0.5);

            // Populate fields with existing medication data
            NameEntry.Text = _medication.Name;
            DosageEntry.Text = _medication.Dosage;
            FrequencyEntry.Text = _medication.Frequency;
        }

        private void SaveMedication(object sender, EventArgs e)
        {
            // Update medication details
            _medication.Name = NameEntry.Text;
            _medication.Dosage = DosageEntry.Text;
            _medication.Frequency = FrequencyEntry.Text;

            // Update the database
            _databaseManager.UpdateMedication(_medication);

            // Reload the medications in the main page
            _reloadMedicationsCallback?.Invoke();

            Close();
        }

        private void ClosePopup(object sender, EventArgs e)
        {
            Close();
        }
    }
}
