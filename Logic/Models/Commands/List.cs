using Repositories.RepositoryModels;

namespace Logic.Models.Commands
{
    public class List : BaseCommand
    {
        const string usage = "list <object>";

        #region Constructor
        public List() : base("list", usage)
        {
            SupportedObjects = new()
            {
                ["profile"] = null
            };
        }
        #endregion

        #region Singleton Pattern
        static List? Instance;
        public static List GetInstance()
        {
            if (Instance == null) Instance = new();
            return Instance;
        }
        #endregion

        #region Execute
        public override void Execute(params object[] args)
        {
            string invalidUsage = "Invalid usage of : list";

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
                    ProfileRepository repository = new();
                    var profileIds = repository.GetAll();

                    if(profileIds.Count < 1)
                    {
                        Console.WriteLine("No profile found");
                        return;
                    }

                    foreach(var item in profileIds)
                    {
                        Console.WriteLine(item);
                    }
                    return;
            }
        }
        #endregion    
    }
}
