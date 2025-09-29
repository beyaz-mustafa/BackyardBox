namespace ConsoleInterface.Validation
{
    // Static class for validating application directories and configuration files
    public static class Validator
    {
        // Path to the application's local data folder
        static readonly string appPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "BackyardBox");

        // Ensure the main application directory exists
        public static void ValidateAppDirectory()
        {
            Directory.CreateDirectory(appPath);
        }

        // Ensure the Profile directory exists
        public static void ValidateProfileDirectory()
        {
            Directory.CreateDirectory(Path.Combine(appPath, "Profiles"));
        }

        // Ensure the user configuration file exists
        public static void ValidateUserConfig()
        {
            var file = Directory.GetFiles(appPath, "user.config", SearchOption.AllDirectories).FirstOrDefault();
            if (string.IsNullOrEmpty(file))
            {
                // Copy default user.config from source if missing
                // File.Create(Path.Combine(appPath, "user.config"));
            }
        }

        // Ensure the application configuration file exists
        public static void ValidateAppConfig()
        {
            var file = Directory.GetFiles(appPath, "app.config", SearchOption.AllDirectories).FirstOrDefault();
            if (string.IsNullOrEmpty(file))
            {
                // Copy default app.config from source if missing
                // File.Create(Path.Combine(appPath, "app.config"));
            }
        }

        // Get the list of profile directories stored in the application
        public static List<string> GetProfiles()
        {
            // Get profile directories within profiles directory
            string[] profileDirectories = Directory.GetDirectories(Path.Combine(appPath, "Profiles"), "*", SearchOption.TopDirectoryOnly);
            
            List<string> profiles = new();

            // Add file that contains info about that profile
            foreach(var path in profileDirectories)
            {
                string? filePath = Directory.GetFiles(path,"*", SearchOption.TopDirectoryOnly).FirstOrDefault();

                // If directory exists but file is missing, delete directory recursivly
                if (string.IsNullOrEmpty(filePath))
                {
                    Directory.Delete(path, true);
                    continue;
                }
                profiles.Add(filePath);
            }

            return profiles;
        }
    }
}
