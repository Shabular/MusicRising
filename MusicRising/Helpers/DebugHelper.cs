namespace MusicRising.Helpers;

public class DebugHelper
{
    private bool _debugMode = true;
    
    
    public void DebugWriteLine(string errorMessage)
    {
        if (_debugMode)
        {
            Console.WriteLine("\n\n\n" + errorMessage + "\n\n\n");
        }
        
    }
}