namespace JazzPianoApp.Models
{
    public class JazzProgression
    {
        public string Name { get; set; } = string.Empty;
        public List<string> ChordTypes { get; set; } = new();
        public List<string> RomanNumerals { get; set; } = new();
    }
}
