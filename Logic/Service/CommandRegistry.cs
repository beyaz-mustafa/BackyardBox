using Logic.Models.Commands;

namespace Logic.Service
{
    // Handles registration of all available commands
    public static class CommandRegistry
    {
        // Clears and populates the global command collection
        public static void RegisterAll()
        {
            // Clear any previously registered commands
            CommandCollection.Commands.Clear();

            // Add new command instances to the collection
            CommandCollection.Commands.AddRange(new Command[]
            {
                Add.GetInstance(),
                Create.GetInstance()
            });
        }
    }
}
