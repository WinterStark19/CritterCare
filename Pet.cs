using System;
using SQLite;

public class Pet
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "Nameless One";  // Default value for Name

    public DateTime BirthDate { get; set; } = DateTime.Now;  // Default value for BirthDate

    public string Species { get; set; } = "Doge Coin";  // Default value for Species

    public string? Breed { get; set; }
}