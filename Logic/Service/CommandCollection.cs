using Logic.Models.Commands;

namespace Logic.Service
{
    // Static collection to hold all available Command objects
    public static class CommandCollection
    {
        public static List<BaseCommand> GetCommands()
        {
            return new List<BaseCommand>()
            {
                Add.GetInstance(),
                Clear.GetInstance(),
                Create.GetInstance(),
                Exit.GetInstance(),
                List.GetInstance()
            };
        }
    }
}
