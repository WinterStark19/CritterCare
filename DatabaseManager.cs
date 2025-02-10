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
    public void DeleteAppointment(int id) => _database.Delete<Appointment>(id);

    // Medication methods
    public void InsertMedication(Medication medication) => _database.Insert(medication);
    public List<Medication> GetMedicationsForPet(int petId) => _database.Table<Medication>().Where(m => m.PetId == petId).ToList();
    public void DeleteMedication(int id) => _database.Delete<Medication>(id);
}

public class Appointment
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int PetId { get; set; } // Foreign key
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
