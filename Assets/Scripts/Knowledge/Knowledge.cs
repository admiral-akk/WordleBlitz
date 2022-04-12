using System.Collections.Generic;

public abstract class Knowledge 
{
    private Word _answer;
    private class CharacterKnowledge
    {
        private Dictionary<char, LetterKnowledge> _knowledge;

        public CharacterKnowledge()
        {
            _knowledge = new Dictionary<char, LetterKnowledge>();
        }

        public LetterKnowledge this[char c]
        {
            get => _knowledge.ContainsKey(c) ? _knowledge[c] : LetterKnowledge.None;
            set
            {
                if (!_knowledge.ContainsKey(c))
                {
                    _knowledge[c] = value;
                    return;
                }
                if (_knowledge[c] == LetterKnowledge.NotHere)
                    return;
                if (_knowledge[c] == LetterKnowledge.Here)
                    return;
                _knowledge[c] = value;
            }
        }
    }

    private List<CharacterKnowledge> _characterKnowledge;

    public enum LetterKnowledge
    {
        None,
        NotInWord,
        NotHere,
        CouldBeHere,
        Here
    }

    public Knowledge(int wordLength)
    {
        _characterKnowledge = new List<CharacterKnowledge>();
        for (var i = 0; i < wordLength; i++)
            _characterKnowledge.Add(new CharacterKnowledge());
    }

    public void UpdateKnowledge(Word guess)
    {
        _guesses.Add(guess);

        for (var i = 0; i < guess.Length; i++)
        {
            var c = guess[i];
            if (c == _answer[i])
            {
                this[i,c] = LetterKnowledge.Here;
                continue;
            }
            if (!_answer.Contains(c))
            {
                 this[c] = LetterKnowledge.NotInWord;
                continue;
            }

            this[i,c] = LetterKnowledge.NotHere;
            this[c] = LetterKnowledge.CouldBeHere;
        }
    }

    private LetterKnowledge this[char c]
    {
        set
        {
            foreach (var knowledge in _characterKnowledge)
                knowledge[c] = value;
        }
    }

    protected LetterKnowledge this[int i, char c]
    {
        get
        {
            return _characterKnowledge[i][c];
        }
        private set
        {
            _characterKnowledge[i][c] = value;
        }
    }
}
