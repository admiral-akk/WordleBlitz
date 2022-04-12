using System.Collections.Generic;
using static CharacterKnowledge;

public class WordLevelKnowledge
{
    private Dictionary<char, LetterKnowledge> _knowledge;

    public WordLevelKnowledge(List<CharacterKnowledge> characters)
    {
        _knowledge = new Dictionary<char, LetterKnowledge>();
        foreach (var c in Language.Alphabet)
        {
            foreach (var k in characters)
            {
                this[c] = k[c];
            }
        }
    }

    private bool CanOverride(LetterKnowledge current, LetterKnowledge other) {
        switch (current)
        {
            default:
                return false;
            case LetterKnowledge.NoKnowledge:
                return true;
            case LetterKnowledge.NotHere:
                return other == LetterKnowledge.CouldBeHere || other == LetterKnowledge.Here;
            case LetterKnowledge.CouldBeHere:
                return other == LetterKnowledge.Here;
        }
    }

    public LetterKnowledge this[char c]
    {
        get => _knowledge.ContainsKey(c) ? _knowledge[c] : LetterKnowledge.NoKnowledge;
        set
        {
            if (!_knowledge.ContainsKey(c))
            {
                _knowledge[c] = value;
                return;
            }
            if (CanOverride(_knowledge[c], value))
                _knowledge[c] = value;
        }
    }
}