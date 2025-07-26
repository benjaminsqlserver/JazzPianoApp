namespace JazzPianoApp.Models
{
    public class PianoKey
    {
        public string Note { get; set; } = string.Empty;
        public bool IsBlack { get; set; }
        public int MidiNote { get; set; }
        public double Frequency { get; set; }
        public bool IsHighlighted { get; set; }
    }
}
