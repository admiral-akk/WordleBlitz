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
                case LetterKnowledge.NoKnowledge:
                case LetterKnowledge.CouldBeHere:
                    _knowledge[c] = value;
                    return;
            }
        }
    }

    public void Clear()
    {
        _knowledge.Clear();
    }
}