namespace MusicRising.Helpers;

using System.Diagnostics;

public static class DockerHelper
{
    public static void StartContainer(string imageName)
    {
        Console.WriteLine("MongoDB is being started, first runs can take longer.");
        if (imageName == "MongoDB")
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
            Console.WriteLine("MongoDB docker up and running");
        }  
    }
        
}
