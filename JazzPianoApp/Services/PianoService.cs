using JazzPianoApp.Models;

namespace JazzPianoApp.Services;

public class PianoService
{
    private readonly Dictionary<string, int[]> _chordIntervals = new()
    {
        { "maj7", new[] { 0, 4, 7, 11 } },
        { "m7", new[] { 0, 3, 7, 10 } },
        { "7", new[] { 0, 4, 7, 10 } },
        { "m7b5", new[] { 0, 3, 6, 10 } },
        { "dim7", new[] { 0, 3, 6, 9 } }
    };

    private readonly Dictionary<string, int> _romanToScaleDegree = new()
    {
        { "I", 1 }, { "ii", 2 }, { "iii", 3 }, { "IV", 4 }, { "V", 5 }, { "vi", 6 }, { "vii", 7 },
        { "i", 1 }, { "VI", 6 }
    };

    public Dictionary<string, JazzProgression> GetJazzProgressions()
    {
        return new Dictionary<string, JazzProgression>
        {
            {
                "ii-V-I",
                new JazzProgression
                {
                    Name = "ii-V-I Major",
                    ChordTypes = new List<string> { "m7", "7", "maj7" },
                    RomanNumerals = new List<string> { "ii", "V", "I" }
                }
            },
            {
                "ii-V-i",
                new JazzProgression
                {
                    Name = "ii-V-i Minor",
                    ChordTypes = new List<string> { "m7b5", "7", "m7" },
                    RomanNumerals = new List<string> { "ii", "V", "i" }
                }
            },
            {
                "I-vi-ii-V",
                new JazzProgression
                {
                    Name = "I-vi-ii-V (Circle of Fifths)",
                    ChordTypes = new List<string> { "maj7", "m7", "m7", "7" },
                    RomanNumerals = new List<string> { "I", "vi", "ii", "V" }
                }
            },
            {
                "vi-ii-V-I",
                new JazzProgression
                {
                    Name = "vi-ii-V-I",
                    ChordTypes = new List<string> { "m7", "m7", "7", "maj7" },
                    RomanNumerals = new List<string> { "vi", "ii", "V", "I" }
                }
            },
            {
                "I-VI-ii-V",
                new JazzProgression
                {
                    Name = "I-VI-ii-V (Rhythm Changes)",
                    ChordTypes = new List<string> { "maj7", "7", "m7", "7" },
                    RomanNumerals = new List<string> { "I", "VI", "ii", "V" }
                }
            }
        };
    }

    public List<PianoKey> GenerateKeys()
    {
        var notes = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        var keys = new List<PianoKey>();

        // Generate 61 keys from C2 to C7
        for (int octave = 2; octave <= 6; octave++)
        {
            for (int noteIndex = 0; noteIndex < 12; noteIndex++)
            {
                var note = notes[noteIndex];
                var midiNote = (octave + 1) * 12 + noteIndex;

                if (midiNote >= 36 && midiNote <= 96 && keys.Count < 61)
                {
                    keys.Add(new PianoKey
                    {
                        Note = $"{note}{octave}",
                        IsBlack = note.Contains('#'),
                        MidiNote = midiNote,
                        Frequency = 440 * Math.Pow(2, (midiNote - 69) / 12.0)
                    });
                }
            }
        }

        return keys.Take(61).ToList();
    }

    public List<string> GetScaleDegrees(string key)
    {
        var chromaticNotes = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        var keyIndex = Array.IndexOf(chromaticNotes, key);
        var majorScaleIntervals = new[] { 0, 2, 4, 5, 7, 9, 11 };

        return majorScaleIntervals.Select(interval =>
            chromaticNotes[(keyIndex + interval) % 12]).ToList();
    }

    public List<string> GetChordNotes(string key, string romanNumeral, string chordType)
    {
        var scale = GetScaleDegrees(key);
        var chromaticNotes = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        var scaleDegree = _romanToScaleDegree[romanNumeral];
        var rootNote = scale[scaleDegree - 1];
        var rootIndex = Array.IndexOf(chromaticNotes, rootNote);

        return _chordIntervals[chordType].Select(interval =>
            chromaticNotes[(rootIndex + interval) % 12]).ToList();
    }

    public ChordInfo GetChordInfo(string key, string romanNumeral, string chordType)
    {
        var scale = GetScaleDegrees(key);
        var scaleDegree = _romanToScaleDegree[romanNumeral];
        var rootNote = scale[scaleDegree - 1];
        var notes = GetChordNotes(key, romanNumeral, chordType);

        return new ChordInfo
        {
            Name = $"{rootNote}{chordType}",
            Notes = notes,
            RomanNumeral = romanNumeral,
            ChordType = chordType
        };
    }
}