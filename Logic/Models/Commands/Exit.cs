namespace Logic.Models.Commands
{
    public sealed class Exit : BaseCommand
    {
        const string usage = "exit";

        #region Constructor
        Exit() : base("exit", usage) { }
        #endregion

        #region Singleton Pattern
        static Exit? Instance;
        public static Exit GetInstance()
        {
            if (Instance == null) Instance = new();
            return Instance;
        }
        #endregion

        #region Execute
        public override void Execute(params object[] args)
        {
            Environment.Exit(0);
        }
        #endregion    
    }
}
