using Repositories.RepositoryModels;
using System.Text;

namespace Logic.Models.Commands
{
    public sealed class Create : BaseCommand
    {
        const string usage = "create <object> [options]";

        #region Constructor
        Create() : base("create", usage)
        {
            // Define supported objects for the "create" command
            SupportedObjects = new()
            {
                ["profile"] = null, // Can create a profile
                ["help"] = null     // Can display help
            };
        }
        #endregion

        #region Singleton Pattern
        static Create? Instance;
        public static Create GetInstance()
        {
            if (Instance == null) Instance = new();
            return Instance;
        }
        #endregion

        #region Execute
        public override void Execute(params object[] args)
        {
            string invalidUsage = "Invalid usage of: create";

            if (args.Length < 1)
            {
                Console.WriteLine(invalidUsage + '\n');
                return;
            }

            var pair = SupportedObjects!.FirstOrDefault(o => o.Key == args[0].ToString());


            if (pair.Key == null || (pair.Value == null && args.Length > 1))
            {
                Console.WriteLine(invalidUsage + '\n');
                return;
            }

            switch (pair.Key)
            {
                case "profile":
                    ProfileRepository profileRepository = new();
                    Console.WriteLine("created profile : " + profileRepository.Create().Id);
                    return;

                case "help":
                    var helpMessage = new StringBuilder(Usage);

                    foreach (var item in SupportedObjects!)
                    {
                        helpMessage.AppendLine("\n\t" + item.Key);

                        // If there are specific arguments for the object, display them
                        if (item.Value != null && item.Value.Count > 0)
                        {
                            foreach (var arg in item.Value)
                            {
                                helpMessage.AppendLine($"\t\t\t{arg.ShortName} {arg.FullName}");
                            }
                        }
                    }

                    Console.WriteLine(helpMessage);
                    return;
            }
            return;
        }
        #endregion    
    }
}
