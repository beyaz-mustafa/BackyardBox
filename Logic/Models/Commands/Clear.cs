namespace Logic.Models.Commands
{
    public sealed class Clear : BaseCommand
    {
        const string usage = "clear";

        #region Constructor
        Clear() : base("clear", usage) { }
        #endregion

        #region Singleton Pattern
        static Clear? Instance;
        public static Clear GetInstance() 
        {
            if (Instance == null) Instance = new();
            return Instance;
        }
        #endregion

        #region Execute
        public override void Execute(params object[] args)
        {
            Console.Clear();
        }
        #endregion    
    }
}
