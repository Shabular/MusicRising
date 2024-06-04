namespace MusicRising.Helpers;

using System.Diagnostics;

public static class DockerHelper
{
    public static void StartContainer(string imageName)
    {
        Console.WriteLine("MAriaDB is being started, first runs can take longer.");
        if (imageName == "MariaDB")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("docker-compose")
            {
                Arguments = $"up -d",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            Process process = new Process
            {
                StartInfo = startInfo,
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine(result);
            Console.WriteLine("MariaDB docker up and running, please wait while the database is built");
            Thread.Sleep(15000); // 150,000 milliseconds = 150 seconds
            
            
        }  
    }
        
}
