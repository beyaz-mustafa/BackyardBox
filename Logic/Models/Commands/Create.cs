using Repositories.RepositoryModels;
using System.Text;

namespace Logic.Models.Commands
{
    // Command class for creating objects like profiles
    public class Create : Command
    {
        const string usage = "create <object> [options]";

        // Private constructor for singleton pattern
        Create() : base("create", usage)
        {
            // Define supported objects for the "create" command
            SupportedObjects = new()
            {
                ["profile"] = null, // Can create a profile
                ["help"] = null     // Can display help
            };
        }

        // Singleton instance
        public static Create? Instance;

        // Get the singleton instance of Create
        public static Create GetInstance()
        {
            if (Instance == null) Instance = new();
            return Instance;
        }

        // Execute the command with provided arguments
        public override object Execute(params object[] args)
        {
            string invalidUsage = "Invalid usage of: create";

            // No arguments provided
            if (args.Length < 1)
            {
                Console.WriteLine(invalidUsage);
                return 0;
            }

            // First argument is empty or whitespace
            if (args[0].GetType() == typeof(string) && string.IsNullOrWhiteSpace((string)args[0]))
            {
                Console.WriteLine(invalidUsage);
                return 0;
            }

            // Find the object type from supported objects
            var pair = SupportedObjects!.FirstOrDefault(o => o.Key == (string)args[0]);

            /* If the user prompts something like this, following statment will work:
             * 
             * create <mistype or unknown object>
             *      pair.key will be null
             *      
             * create <object> [mistype or unknown option]
             *      pair.key not null but, pair.value is null and user prompted something after <object>
             * 
             * This works in most prompts beside : create [option]
             * Later, accepting create [option] (--help etc.) prompt, will lead to implement changes
             */

            if (pair.Key == null || (pair.Value == null && args.Length > 1))
            {
                Console.WriteLine(invalidUsage);
                return 0;
            }

            // Execute logic based on the object type
            switch (pair.Key)
            {
                case "profile":
                    // Create a new profile
                    ProfileRepository profileRepository = new();
                    return profileRepository.Create();

                case "help":
                    // Display usage/help message
                    var helpMessage = new StringBuilder(Usage);

                    foreach (var item in SupportedObjects!)
                    {
                        helpMessage.AppendLine("\n\t\t" + item.Key);

                        // If there are specific arguments for the object, display them
                        if (item.Value != null && item.Value.Count > 0)
                        {
                            foreach (var arg in item.Value)
                            {
                                helpMessage.AppendLine($"\t\t\t\t{arg.ShortName} {arg.FullName}");
                            }
                        }
                    }

                    Console.WriteLine(helpMessage);
                    return 0;
            }

            return 0;
        }
    }
}
