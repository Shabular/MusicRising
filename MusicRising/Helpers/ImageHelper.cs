using MusicRising.Models;

namespace MusicRising.Helpers;

public class ImageHelper
{
    public static string SaveImageToServer(IWebHostEnvironment webHostEnvironment, IFormFile? image)
    {
        if (image == null) return "Default.png";
        // get the filnamen and add to the foldername to create a place to store band/venue pics
        string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Images");
            
        string fileName = Guid.NewGuid().ToString() + "_" + image.FileName; // this is used to be able to have multiple same filenames just in case
        string filePath = Path.Combine(uploadDir, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            image.CopyTo(fileStream);
        }

        return fileName;

    }
}

