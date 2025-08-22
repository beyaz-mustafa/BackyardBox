namespace ConsoleInterface.Validation
{
    public static class BackupValidator
    {
        // Get <username>/appdata/local path
        // Replace '\' with '/'
        // Append app's folder name
        public static readonly string appPath = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            .Replace('\\', '/') + "/BackyarBox";

        public static void ValidateBackupFolder()
        {
            if (!Directory.Exists(appPath+"/profiles"))
            {
                Directory.CreateDirectory(appPath + "/profiles");
            }
        }
    }
}
