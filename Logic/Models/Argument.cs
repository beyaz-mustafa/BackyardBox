namespace Logic.Models
{
    // Represents a command-line argument with optional short and full names
    public class Argument
    {
        public string? ShortName { get; set; } = null;
        public string? FullName { get; set; } = null;
    }
}
