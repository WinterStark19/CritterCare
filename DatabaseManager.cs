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
        _database.CreateTable<Pet>(); // Ensure table is created
    }

    public void InsertPet(Pet pet)
    {
        _database.Insert(pet);
    }

    public List<Pet> GetPets()
    {
        return _database.Table<Pet>().ToList();
    }

    public void UpdatePet(Pet pet)
    {
        _database.Update(pet);
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

