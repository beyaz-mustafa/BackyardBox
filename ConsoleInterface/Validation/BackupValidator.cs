namespace ConsoleInterface.Validation
{
    public static class BackupValidator
    {
        public static readonly string appPath = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            + "\\BackyarBox";

        public static void ValidateBackupFolder()
        {
            Directory.CreateDirectory(appPath + "\\Profiles");
        }
        public static bool CheckExistingProfiles()
        {
            return Directory.GetFiles(appPath + "\\Profiles").Length > 1 ? true : false;
        }
    }
}