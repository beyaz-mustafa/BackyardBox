namespace ConsoleInterface.Validation
{
    public static class ConfigValidator
    {
        public static readonly string appPath = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            + "\\BackyarBox";

        public static void ValidateAppDirectory()
        {
            Directory.CreateDirectory(appPath);
        }
        public static void ValidateUserConfig()
        {
            if(!File.Exists(appPath+"\\user.config"))
            {
                // Copy default user.config from source
            }
        }
        public static void ValidateAppConfig()
        {
            if(!File.Exists(appPath+"\\app.config"))
            {
                // Copy default app.config from source
            }
        }
    }
}
