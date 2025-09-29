namespace Logic.Models.Commands
{
    /* Example command class
     * - Serves as a guide/template when creating new command classes
     */

    // Every command class must derive from the BaseCommand abstract class
    public sealed class ExampleClass : BaseCommand
    {
        /* In actual command classes:
         * - Constructors are private (singleton pattern).
         * - Instances are obtained via Logic/Service/CommandCollection through the GetCommand() method.
         * 
         * Since this class inherits from BaseCommand, it must define CommandName and Usage.
         */
        const string usage = "<usage>";
        ExampleClass() : base("<commandName>", usage)
        {
            // SupportedObjects is inherited from BaseCommand.
            // Its purpose is to describe which objects this command operates on.
            //
            // Type: Dictionary<string, List<Argument>?>?
            // - Nullable because some commands don’t need it (e.g., exit, clear).
            // - Dictionary values are nullable to support cases where
            //   an object exists but doesn’t require arguments.
            SupportedObjects = new()
            {
                ["<objectName>"] = new()
                {
                    new() { ShortName = "<shortName>", FullName = "<fullName>" }
                }
            };
        }

        // Singleton pattern implementation
        static ExampleClass? Instance;
        public static ExampleClass GetInstance()
        {
            if (Instance == null) Instance = new();
            return Instance;
        }

        // Execute method comes from BaseCommand.
        // Declared with params object[] to allow flexible argument types.
        public override void Execute(params object[] args)
        {
            // Always validate args length to avoid logic errors
            string invalidUsage = "invalid usage of : <commandName>";
            if (args.Length < 1)
            {
                Console.WriteLine(invalidUsage + '\n');
                return;
            }

            // Find if the first argument matches any supported object
            var pair = SupportedObjects!.FirstOrDefault(o => o.Key == args[0].ToString());

            /* Example scenarios:
             * 
             * <commandName> <mistyped_or_unknown_object>
             *     => pair.Key will be null
             * 
             * <commandName> <object> [mistyped_or_unknown_option]
             *     => pair.Key not null, but pair.Value is null 
             *        and user supplied extra arguments
             * 
             * Works for most cases except:
             * <commandName> [option_only]
             * 
             * Handling [option_only] cases (e.g., --help) requires additional logic later.
             */
            if (pair.Key == null || (pair.Value == null && args.Length > 1))
            {
                Console.WriteLine(invalidUsage + '\n');
                return;
            }

            // Main logic: decide behavior for <commandName> <object> [options]
            switch (pair.Key)
            {
                case "<supportedObject01>":
                    return;
                case "<supportedObject02>":
                    return;
            }

            #region IMPORTANT
            // After completing your command class,
            // add it to CommandCollection.
            // Otherwise, the interface will never dispatch user input to it.
            #endregion
        }
    }
}
