using System.Runtime.CompilerServices;

namespace Logic.Models.Commands
{
    // Abstract base class for all commands
    public abstract class Command
    {
        protected Command(string commandName, string usage)
        {
            CommandName = commandName;
            Usage = usage;
        }

        // Properties
        public string CommandName { get; init; }
        public string Usage { get; init; }
        public Dictionary<string, List<Argument>?>? SupportedObjects { get; protected init; }

        // Methods
        public abstract object Execute(params object[] args);

        // Helper method to print an error message with source file and line number
        public void Error(string message, [CallerFilePathAttribute] string path = "", [CallerLineNumberAttribute] int number = 0)
        {
            Console.WriteLine(string.Join(' ', message, path, number));
        }

        // Commented-out method for parsing command-line style arguments
        //public List<string> ParseArguments(string prompt)
        //{
        //    if (string.IsNullOrWhiteSpace(prompt))
        //    {
        //        return new List<string>();
        //    }
        //
        //    // Match arguments, supporting quoted strings
        //    var matches = Regex.Matches(prompt, @"[^""'\s]+|""([^""]*)""|'([^']*)'");
        //    var args = new List<string>();
        //
        //    foreach (Match match in matches)
        //    {
        //        if (match.Groups[1].Success)
        //        {
        //            args.Add(match.Groups[1].Value);
        //        }
        //        else if (match.Groups[2].Success)
        //        {
        //            args.Add(match.Groups[2].Value);
        //        }
        //        else
        //        {
        //            args.Add(match.Value);
        //        }
        //    }
        //
        //    // Skip first argument, typically the command itself
        //    return args.Skip(1).ToList();
        //}
    }
}
