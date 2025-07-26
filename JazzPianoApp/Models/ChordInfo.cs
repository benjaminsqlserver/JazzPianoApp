namespace JazzPianoApp.Models
{
    public class ChordInfo
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Notes { get; set; } = new();
        public string RomanNumeral { get; set; } = string.Empty;
        public string ChordType { get; set; } = string.Empty;
    }
}
