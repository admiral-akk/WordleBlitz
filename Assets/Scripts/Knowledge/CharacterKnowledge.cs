using System.Collections.Generic;

public class CharacterKnowledge
{
    public enum LetterKnowledge
    {
        NoKnowledge,
        NotInWord,
        NotHere,
        CouldBeHere,
        Here
    }
    private Dictionary<char, LetterKnowledge> _knowledge;

    public CharacterKnowledge()
    {
        _knowledge = new Dictionary<char, LetterKnowledge>();
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
                    // This is to handle the case where we're merging knowledge.
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

    public void Merge(CharacterKnowledge other)
    {
        foreach (var k in other._knowledge.Keys)
            this[k] = other[k];
    }
}