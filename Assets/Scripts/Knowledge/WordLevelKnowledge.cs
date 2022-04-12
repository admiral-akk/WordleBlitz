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
            switch (_knowledge[c])
            {
                default:
                    break;
                case LetterKnowledge.NotHere:
                    if (value != LetterKnowledge.Here && value != LetterKnowledge.CouldBeHere)
                        return;
                    break;
                case LetterKnowledge.NotInWord:
                case LetterKnowledge.Here:
                    return;
            }
            _knowledge[c] = value;
        }
    }
}