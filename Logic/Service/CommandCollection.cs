using Logic.Models.Commands;

namespace Logic.Service
{
    // Static collection to hold all available Command objects
    public static class CommandCollection
    {
        // List storing registered commands
        public static List<Command> Commands = new();
    }
}
