using ConsoleInterface.Validation;
using Logic.Models.Commands;
using Logic.Service;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text.RegularExpressions;

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
            Console.WriteLine("Validating app directory...\n");
            Validator.ValidateAppDirectory();
            Validator.ValidateProfileDirectory();
            Validator.ValidateAppConfig();
            Validator.ValidateUserConfig();

            // Get profiles if they exist
            var profiles = Validator.GetProfiles();

            if (profiles.Count > 0)
            {
                Console.WriteLine($"{profiles.Count} profiles found. Syncing not implemented\n");
                // TODO: load profiles and start sync logic
            }

            var commands = CommandCollection.GetCommands();

            while (true)
            {
                Console.Write("BackyardBox" + '>');
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;

                var arguments = ParseArguments(input);

                if (arguments.Count > 0)
                {
                    BaseCommand? command = commands.FirstOrDefault(c => c!.CommandName == arguments.First().ToString(), null);

                    if (command != null) command.Execute(arguments.Skip(1).ToArray());
                }
            }
        }

        // Parsing user input
        static List<object> ParseArguments(string prompt)
        {
            // Return empty list if the input is null, empty, or whitespace
            if (string.IsNullOrWhiteSpace(prompt)) return new();

            // Regex matches tokens: words, or quoted strings (single/double)
            var matches = Regex.Matches(prompt, @"[^""'\s]+|""([^""]*)""|'([^']*)'");
            var args = new List<object>();

            foreach (Match match in matches)
            {
                if (match.Groups[1].Success) // Double-quoted string
                {
                    args.Add(new object[] { match.Groups[1].Value.ToLower() });
                }
                else if (match.Groups[2].Success) // Single-quoted string
                {
                    args.Add(new object[] { match.Groups[2].Value.ToLower() });
                }
                else // Unquoted word
                {
                    args.Add(match.Value.ToLower());
                }
            }

            return args;
        }

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

        #region Dll import
        // Import Unix function to get user ID
        [DllImport("libc")]
        static extern uint getuid(); 
        #endregion
    }
}
