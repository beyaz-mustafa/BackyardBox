using ConsoleInterface.Validation;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ConsoleInterface
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Check if the program is running with administrator/root privileges
            if (!IsAdmin())
            {
                Console.WriteLine("Please start the app as administrator");
                Console.Write("\nPress any key to close the program...");
                Console.ReadKey(true); // Wait for a key press before exiting
                return;
            }

            // Validate and prepare application environment
            Validator.ValidateAppDirectory();
            Validator.ValidateAppConfig();
            Validator.ValidateUserConfig();

            // Get profiles if they exist
            var profiles = Validator.GetProfiles();

            if (profiles.Count > 0)
            {
                // TODO: load profiles and start sync logic
            }
        }

        #region Administrator check
        // Cross-platform method to check if running with admin privileges
        static bool IsAdmin()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // On Windows, check if the current identity belongs to the Administrator role
                WindowsPrincipal principal = new(WindowsIdentity.GetCurrent());
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            // On Unix-based systems, check if UID is 0 (root)
            return getuid() == 0;
        }

        // Import Unix function to get user ID
        [DllImport("libc")]
        static extern uint getuid();
        #endregion
    }
}
