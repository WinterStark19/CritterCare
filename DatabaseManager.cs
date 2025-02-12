using SQLite;
using System.IO;
using System.Collections.Generic;

public class DatabaseManager
{
    private SQLiteConnection _database;

    public DatabaseManager()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydatabase.db3");
        _database = new SQLiteConnection(dbPath);

        // Ensure tables are created
        _database.CreateTable<Pet>();
        _database.CreateTable<Appointment>();
        _database.CreateTable<Medication>();
    }

    // Pet methods
    public void InsertPet(Pet pet) => _database.Insert(pet);
    public List<Pet> GetPets() => _database.Table<Pet>().ToList();
    public void UpdatePet(Pet pet) => _database.Update(pet);
    public void DeletePet(int id)
    {
        var pet = _database.Find<Pet>(id);
        if (pet != null)
        {
            _database.Delete(pet);
            _database.Execute("DELETE FROM Appointment WHERE PetId = ?", id);
            _database.Execute("DELETE FROM Medication WHERE PetId = ?", id);
        }
    }

    // Appointment methods
    public void InsertAppointment(Appointment appointment) => _database.Insert(appointment);
    public List<Appointment> GetAppointmentsForPet(int petId) => _database.Table<Appointment>().Where(a => a.PetId == petId).ToList();
    public void UpdateAppointment(Appointment appointment) => _database.Update(appointment);
    public void DeleteAppointment(int id) => _database.Delete<Appointment>(id);

    // Medication methods
    public void InsertMedication(Medication medication) => _database.Insert(medication);
    public List<Medication> GetMedicationsForPet(int petId) => _database.Table<Medication>().Where(m => m.PetId == petId).ToList();

    public void UpdateMedication(Medication medication) => _database.Update(medication);
    public void DeleteMedication(int id) => _database.Delete<Medication>(id);

    public List<AppointmentWithPet> GetAppointmentsForWeek(DateTime startOfWeek)
    {
        // Calculate the end date of the week
        var endOfWeek = startOfWeek.AddDays(6);

        // Get all appointments that fall within the week
        var appointments = _database.Table<Appointment>()
            .Where(a => a.Date >= startOfWeek && a.Date <= endOfWeek)
            .ToList();

        // Create a list of AppointmentWithPet by joining each appointment with the corresponding pet's name
        var appointmentsWithPet = appointments.Select(a => new AppointmentWithPet
        {
            Id = a.Id,
            PetId = a.PetId,
            Title = a.Title,
            Date = a.Date,
            Notes = a.Notes,
            PetName = _database.Table<Pet>().FirstOrDefault(p => p.Id == a.PetId)?.Name
        }).ToList();

        return appointmentsWithPet;
    }

    public class AppointmentWithPet
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }  
        public string Notes { get; set; }
        public string PetName { get; set; }  
    }
}

public class Appointment
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int PetId { get; set; } 
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }
}

public class Medication
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int PetId { get; set; }
    public string Name { get; set; }
    public string Dosage { get; set; }
    public string Frequency { get; set; }
}
