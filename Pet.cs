using System;
using System.IO;
using SQLite;
using Microsoft.Maui.Controls;

public class Pet
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "Nameless One";  // Default value for Name

    public DateTime BirthDate { get; set; } = DateTime.Now;  // Default value for BirthDate

    public string Species { get; set; } = "Doge Coin";  // Default value for Species

    public string? Breed { get; set; }

    public double Weight { get; set; } = 0.0;  // Default value for Weight

    [Ignore] // This prevents SQLite from storing the Age field in the database.
    public int Age => (DateTime.Now.Year - BirthDate.Year - (DateTime.Now.DayOfYear < BirthDate.DayOfYear ? 1 : 0));

    public byte[] Image { get; set; } // Image stored as a byte array

    // This property will hold the ImageSource after conversion
    [Ignore]
    public ImageSource ImageSource { get; set; }

    // Constructor to set default values
    public Pet()
    {
        // Load default image from the Resources folder if no image is provided
        Image = LoadDefaultImage();
        // Convert the byte array to an ImageSource when the object is created
        ImageSource = ConvertByteArrayToImageSource(Image);
    }

    // Method to load the default image from the Resources folder
    private byte[] LoadDefaultImage()
    {
        string defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "default_dog.jpg");

        if (File.Exists(defaultImagePath))
        {
            return File.ReadAllBytes(defaultImagePath);  // Read the image file as a byte array
        }

        // Return an empty byte array if the default image is not found
        return new byte[0];
    }

    // Method to convert byte[] to ImageSource
    private ImageSource ConvertByteArrayToImageSource(byte[] byteArray)
    {
        if (byteArray != null && byteArray.Length > 0)
        {
            return ImageSource.FromStream(() => new MemoryStream(byteArray));
        }
        return ImageSource.FromFile("default_image.jpg"); // Fallback image if byte array is null or empty
    }
}
