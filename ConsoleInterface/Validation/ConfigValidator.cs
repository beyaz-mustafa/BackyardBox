namespace ConsoleInterface.Validation
{
    public static class ConfigValidator
    {
        // Get <username>/appdata/local path
        // Replace '\' with '/'
        // Append app's folder name
        public static readonly string appPath = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            .Replace('\\', '/') + "/BackyarBox";

        public static void ValidateAppDirectory()
        {
            if(!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(appPath);
            }
        }
        public static void ValidateUserConfig()
        {
            if(!File.Exists(appPath+"/user.config"))
            {
                // Copy default user.config from source
            }
        }
        public static void ValidateAppConfig()
        {
            if(!File.Exists(appPath+"/app.config"))
            {
                // Copy default app.config from source
            }
        }
    }
}
