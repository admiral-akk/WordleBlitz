
using System;
using System.Collections.Generic;
using UnityEngine;
using static CharacterKnowledge;

public abstract class Knowledge 
{
    private Word _answer;

    protected List<CharacterKnowledge> characterKnowledge;
    protected Dictionary<char, int> characterCount;
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
        Clear();
    }

    public void UpdateKnowledge(Word guess)
    {
        for (var i = 0; i < guess.Length; i++)
        {
            var c = guess[i];
            if (_answer.CountChar(c) < guess.CountChar(c))
                characterCount[c] = _answer.CountChar(c);
            if (!_answer.Contains(c))
            {
                UpdateAll(c, LetterKnowledge.NoMoreInWord);
                continue;
            }
            UpdateAll(c, LetterKnowledge.CouldBeHere);
            if (c == _answer[i])
            {
                characterKnowledge[i][c] = LetterKnowledge.Here;
                continue;
            }
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
        characterCount = new Dictionary<char, int>();
    }
}
