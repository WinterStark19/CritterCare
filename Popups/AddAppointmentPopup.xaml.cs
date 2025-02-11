using CommunityToolkit.Maui.Views;
using System;

namespace CritterCare
{
    public partial class AddAppointmentPopup : Popup
    {
        private readonly int _petId;
        private readonly Action _reloadAppointmentsCallback;
        private readonly DatabaseManager _databaseManager;

        public AddAppointmentPopup(int petId, Action reloadAppointmentsCallback)
        {
            InitializeComponent();
            _petId = petId;
            _reloadAppointmentsCallback = reloadAppointmentsCallback;
            _databaseManager = new DatabaseManager(); // Initialize it

            // Set initial size based on the window dimensions
            var windowWidth = Application.Current.Windows[0].Width;
            var windowHeight = Application.Current.Windows[0].Height;
            this.Size = new Size(windowWidth * 0.5, windowHeight * 0.5);
        }

        private void SaveAppointment(object sender, EventArgs e)
        {
            var newAppointment = new Appointment
            {
                PetId = _petId,
                Title = TitleEntry.Text,
                Date = DatePicker.Date,
                Notes = NotesEditor.Text
            };

            // Save to the database
            _databaseManager.InsertAppointment(newAppointment);

            // Reload the appointments in the main page
            _reloadAppointmentsCallback?.Invoke();

            Close();
        }

        private void ClosePopup(object sender, EventArgs e)
        {
            Close();
        }
    }
}
