using System.Runtime.CompilerServices;

namespace Logic.Models.Commands
{
    // Abstract base class for all commands
    public abstract class BaseCommand
    {
        #region Constructor
        public BaseCommand(string commandName, string usage)
        {
            CommandName = commandName;
            Usage = usage;
        }
        #endregion

        #region Properties
        public string CommandName { get; init; }
        public string Usage { get; init; }
        public Dictionary<string, List<Argument>?>? SupportedObjects { get; protected init; }
        #endregion

        #region Methods
        public abstract void Execute(params object[] args);

        // Helper method to print an error message with source file and line number
        public void Error(string message, [CallerFilePathAttribute] string path = "", [CallerLineNumberAttribute] int number = 0)
        {
            Console.WriteLine(string.Join(' ', message, path, number));
        }
        #endregion    
    }
}
