using static CharacterKnowledge;

public class KeyboardKnowledge : Knowledge
{
    public KeyboardKnowledge(int wordLength) : base(wordLength)
    {
    }

    public LetterKnowledge this[char c]
    {
        get
        {
            var knowledge = new CharacterKnowledge();
            foreach (var k in characterKnowledge)
                knowledge.Merge(k);
            return knowledge[c];
        }
    }
}
