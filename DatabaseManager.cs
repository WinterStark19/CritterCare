using SQLite;
using System.IO;

public class DatabaseManager
{
    private SQLiteConnection _database;

    public DatabaseManager()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydatabase.db3");
        _database = new SQLiteConnection(dbPath);
        _database.CreateTable<Pet>(); // Create the table if it doesn't exist
    }

    public void InsertPet(Pet pet)  // Changed 'person' to 'pet' for consistency
    {
        _database.Insert(pet);
    }

    public List<Pet> GetPets()
    {
        return _database.Table<Pet>().ToList();
    }

    public void DeletePet(int id)
    {
        var pet = _database.Find<Pet>(id);
        if (pet != null)
        {
            _database.Delete(pet);
        }
    }
}

