using ConsoleInterface.Validation;

namespace ConsoleInterface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigValidator.ValidateAppDirectory();
            ConfigValidator.ValidateAppConfig();
            ConfigValidator.ValidateUserConfig();

            BackupValidator.ValidateBackupFolder();
            // if profiles exists, start sync
            //bool exists = BackupValidator.CheckExistingProfiles();
        }
    }
}