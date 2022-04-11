

using System.Collections.Generic;
using System.Linq;

public class GuessKnowledge
{
    private Dictionary<char, WordKnowledge.LetterKnowledge> _wordKnowledge;
    private List<Dictionary<char, WordKnowledge.LetterKnowledge>> _letterKnowledge;

    public GuessKnowledge(int maxLength)
    {
        _wordKnowledge = new Dictionary<char, WordKnowledge.LetterKnowledge>();
        _letterKnowledge = new List<Dictionary<char, WordKnowledge.LetterKnowledge>>();
        while (maxLength-- > 0)
            _letterKnowledge.Add(new Dictionary<char, WordKnowledge.LetterKnowledge>());
    }

    public void Clear()
    {
        _wordKnowledge.Clear();
        foreach (var d in _letterKnowledge)
            d.Clear();
    }

    public void Set(char c, int i, WordKnowledge.LetterKnowledge k)
    {
        if (!_letterKnowledge[i].ContainsKey(c))
            _letterKnowledge[i][c] = k;
    }

    public WordKnowledge.LetterKnowledge Get(char c, int i)
    {
        if (!_letterKnowledge[i].ContainsKey(c))
            return WordKnowledge.LetterKnowledge.Default;
        return _letterKnowledge[i][c];
    }

    public void UpdateKnowledge(Word guess, Word answer)
    {
        for (var i = 0; i < guess.Length; i++)
        {
            var c = guess[i];
            if (!answer.Contains(c))
            {
                Set(c, i, WordKnowledge.LetterKnowledge.None);
                continue;
            }

            if (guess[i] == answer[i])
            {
                Set(c, i, WordKnowledge.LetterKnowledge.Correct);
                continue;
            }

            if (answer.Any(c => c == guess[i]))
            {
                Set(c, i, WordKnowledge.LetterKnowledge.WrongPosition);
                continue;
            }
        }
    }

    public WordKnowledge GenerateKnowledge(Word guess, int maxLength)
    {
        var wordKnowledge = new WordKnowledge(guess, maxLength);
        for (var i = 0; i < guess.Length; i++)
        {
            var c = guess[i];
            if (!_letterKnowledge[i].ContainsKey(c))
                wordKnowledge[i] = WordKnowledge.LetterKnowledge.Default;
            else
                wordKnowledge[i] = _letterKnowledge[i][guess[i]];
        }
        return wordKnowledge;
    }
}