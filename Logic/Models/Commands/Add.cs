using Entities.Models;
using Repositories.RepositoryModels;
using System.Collections;

namespace Logic.Models.Commands
{
    // Command class for adding objects such as backups to profiles
    public class Add : Command
    {
        const string usage = "usage : add <object>, add <object> [options]";

        // Private constructor for singleton pattern
        Add() : base("add", usage)
        {
            // Define supported objects for the "add" command
            SupportedObjects = new()
            {
                ["backup"] = null // Currently supports adding backups
            };
        }

        // Singleton instance
        public static Add? Instance;

        // Get the singleton instance of Add
        public static Add GetInstance()
        {
            if (Instance == null) Instance = new();
            return Instance;
        }

        // Execute the command with provided arguments
        public override object Execute(params object[] args)
        {
            string invalidUsage = "Invalid usage of: add";

            // Check if any arguments were provided
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
             * add <mistype or unknown object>
             *      pair.key will be null
             *      
             * add <object> [mistype or unknown option]
             *      pair.key not null but, pair.value is null and user prompted something after <object>
             * 
             * This works in most prompts beside : add [option]
             * Later, accepting add [option] (--help etc.) prompt, will lead to implement changes
             */
            if (pair.Key == null || (pair.Value == null && args.Length > 1))
            {
                Console.WriteLine(invalidUsage);
                return 0;
            }

            switch (pair.Key)
            {
                /*
                    Arguments for backup object:
                        args[0] : object type ("backup")
                        args[1] : profile Id or alias
                        args[2] : source paths (IEnumerable<string>)
                        args[3] : destination paths (IEnumerable<string>)
                */
                case "backup":
                    // Type check for arguments
                    if (args[1].GetType() != typeof(string) || !(args[2] is IEnumerable) || !(args[3] is IEnumerable))
                    {
                        Error("Something went wrong in logic");
                        return 0;
                    }

                    // Find the profile by Id or alias
                    ProfileRepository profileRepository = new();
                    Profile profile = profileRepository.Find(args[1].ToString());
                    if (profile == null)
                    {
                        Console.WriteLine($"There is no profile called {args[1].ToString()}");
                        return 0;
                    }

                    // Check that all source and destination paths exist
                    foreach (var item in Enumerable.Concat((IEnumerable<string>)args[2], (IEnumerable<string>)args[3]))
                    {
                        if (!Path.Exists(item))
                        {
                            Console.WriteLine($"Path doesn't exists : {item}");
                            return 0;
                        }
                    }

                    // Add the backup to the profile
                    var contents = new List<string>((IEnumerable<string>)args[2]);
                    var destinationPaths = new List<string>((IEnumerable<string>)args[2]);
                    profileRepository.AddBackup(profile, contents, destinationPaths);

                    return 0;
            }

            return 0;
        }
    }
}
