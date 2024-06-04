using MusicRising.Models;

namespace MusicRising.Helpers;

public class ImageHelper
{
    private readonly DebugHelper _debugHelper = new DebugHelper();
    public static string SaveImageToServer(IWebHostEnvironment webHostEnvironment, IFormFile? image)
    {
        if (image == null) return "Default.png";
        // get the filnamen and add to the foldername to create a place to store band/venue pics
            
        string fileName = Guid.NewGuid().ToString() + "_" + image.FileName; // this is used to be able to have multiple same filenames just in case
        string filePath = Path.Combine( webHostEnvironment.WebRootPath + "/Images/",fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            image.CopyTo(fileStream);
        }

        return fileName;

    }
    
    public static string UpdateImageOnServer(IWebHostEnvironment webHostEnvironment, IFormFile? image, string oldFileName)
    {

        try
        {
            // save new image
            Console.Write("try update image");
            string newFileName = SaveImageToServer(webHostEnvironment, image);
            Console.Write("new filename " + newFileName);
            if (newFileName != "Default.png")
            {
                try
                {
                    // remove old file
                    bool isDeleted = RemoveImageFromServer(webHostEnvironment, oldFileName);
                    if (isDeleted){ return newFileName;}
                }catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file: {ex.Message}");
                    return oldFileName;
                }
            }
        }
        catch
        {
            if (oldFileName == null) return "Default.png";
            return oldFileName;
        }
        
        return oldFileName;

    }
    
    public static bool RemoveImageFromServer(IWebHostEnvironment webHostEnvironment, string filename)
    {
        // Get the path to file to delete
        string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "/Images");
        string filePath = Path.Combine(uploadDir, filename);
    
        // Check if the file exists
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
                return false;
            }
        }
    
        // trigged when file not there
        return true;
    }
}

