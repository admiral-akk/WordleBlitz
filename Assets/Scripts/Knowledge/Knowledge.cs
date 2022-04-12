using System.Collections.Generic;
using System.Linq;
using static CharacterKnowledge;

public abstract class Knowledge 
{
    private Word _answer;

    protected List<CharacterKnowledge> characterKnowledge;
    protected int Length => characterKnowledge.Count;

    public Knowledge(int wordLength)
    {
        characterKnowledge = new List<CharacterKnowledge>();
        for (var i = 0; i < wordLength; i++)
            characterKnowledge.Add(new CharacterKnowledge());
    }

    public void SetAnswer(Word answer)
    {
        _answer = answer;
    }

    public void UpdateKnowledge(Word guess)
    {
        for (var i = 0; i < guess.Length; i++)
        {
            var c = guess[i];
            if (c == _answer[i])
            {
                characterKnowledge[i][c] = LetterKnowledge.Here;
                continue;
            }
            if (!_answer.Contains(c))
            {
                UpdateAll(c,LetterKnowledge.NotInWord);
                continue;
            }

            characterKnowledge[i][c] = LetterKnowledge.NotHere;
            UpdateAll(c, LetterKnowledge.CouldBeHere);
        }
    }

    private void UpdateAll(char c, LetterKnowledge k)
    {
        foreach (var knowledge in characterKnowledge)
            knowledge[c] = k;
    }

    public void Clear()
    {
        foreach (var knowledge in characterKnowledge)
            knowledge.Clear();
    }
}
