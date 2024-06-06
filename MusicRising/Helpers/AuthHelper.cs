namespace MusicRising.Helpers;

// this will be used to check if the user may acces this page
// if not redirect to index
public class AuthHelper
{
    public static bool Authorize(string userID, string ownerId)
    {
        if (userID.Contains(ownerId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

