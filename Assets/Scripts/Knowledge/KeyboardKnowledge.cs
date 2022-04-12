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
            var knowledge = new WordLevelKnowledge(characterKnowledge);
            return knowledge[c];
        }
    }
}
