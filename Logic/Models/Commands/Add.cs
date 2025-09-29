using Entities.Models;
using Repositories.RepositoryModels;
using System.Collections;

namespace Logic.Models.Commands
{
    public sealed class Add : BaseCommand
    {
        const string usage = "usage : add <object>, add <object> [options]";

        #region Constructor
        Add() : base("add", usage)
        {
            // Define supported objects for the "add" command
            SupportedObjects = new()
            {
                ["backup"] = null // Currently supports adding backups
            };
        }
        #endregion 
        
        #region Singleton Pattern
        static Add? Instance;
        public static Add GetInstance()
        {
            if (Instance == null) Instance = new();
            return Instance;
        }
        #endregion

        #region Execute
        public override void Execute(params object[] args)
        {
            string invalidUsage = "Invalid usage of: add";

            if (args.Length < 1)
            {
                Console.WriteLine(invalidUsage);
                return;
            }

            var pair = SupportedObjects!.FirstOrDefault(o => o.Key == args[0].ToString());

            if (pair.Key == null || (pair.Value == null && args.Length > 1))
            {
                Console.WriteLine(invalidUsage);
                return;
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
                        return;
                    }

                    // Find the profile by Id or alias
                    ProfileRepository profileRepository = new();
                    Profile? profile = profileRepository.Find(args[1].ToString()!);
                    if (profile == null)
                    {
                        Console.WriteLine($"There is no profile called {args[1].ToString()}");
                        return;
                    }

                    // Check that all source and destination paths exist
                    foreach (var item in Enumerable.Concat((IEnumerable<string>)args[2], (IEnumerable<string>)args[3]))
                    {
                        if (!Path.Exists(item))
                        {
                            Console.WriteLine($"Path doesn't exists : {item}");
                            return;
                        }
                    }

                    // Add the backup to the profile
                    var contents = new List<string>((IEnumerable<string>)args[2]);
                    var destinationPaths = new List<string>((IEnumerable<string>)args[2]);
                    profileRepository.AddBackup(profile, contents, destinationPaths);

                    return;
            }
            return;
        }
        #endregion
    }
}
