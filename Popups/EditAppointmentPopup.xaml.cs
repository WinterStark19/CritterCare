using CommunityToolkit.Maui.Views;
using System;

namespace CritterCare
{
    public partial class EditAppointmentPopup : Popup
    {
        private readonly Appointment _appointment;
        private readonly Action _reloadAppointmentsCallback;
        private readonly DatabaseManager _databaseManager;

        public EditAppointmentPopup(Appointment appointment, Action reloadAppointmentsCallback)
        {
            InitializeComponent();
            _appointment = appointment;
            _reloadAppointmentsCallback = reloadAppointmentsCallback;
            _databaseManager = new DatabaseManager(); // Initialize it

            // Set initial size based on the window dimensions
            var windowWidth = Application.Current.Windows[0].Width;
            var windowHeight = Application.Current.Windows[0].Height;
            this.Size = new Size(windowWidth * 0.5, windowHeight * 0.5);

            // Populate fields with existing appointment data
            TitleEntry.Text = _appointment.Title;
            DatePicker.Date = _appointment.Date;
            NotesEditor.Text = _appointment.Notes;
        }

        private void SaveAppointment(object sender, EventArgs e)
        {
            // Update appointment details
            _appointment.Title = TitleEntry.Text;
            _appointment.Date = DatePicker.Date;
            _appointment.Notes = NotesEditor.Text;

            // Update the database
            _databaseManager.UpdateAppointment(_appointment);

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
