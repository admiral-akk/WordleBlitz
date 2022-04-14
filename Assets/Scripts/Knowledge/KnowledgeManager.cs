using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterKnowledge;

public class KnowledgeManager : BaseManager
{
    [SerializeField] private int WordLength;
    [SerializeField] private int DailyAnswerCount;

    public int Length => WordLength;

    private DictionaryManager _dictionary;
    
    private KeyboardKnowledge _keyboardKnowledge;
    public KeyboardKnowledge KeyboardKnowledge
    {
        get {
            if (_keyboardKnowledge == null)
                _keyboardKnowledge = new KeyboardKnowledge(WordLength);
            return _keyboardKnowledge;
        }
    }
    private GuessKnowledge _guessKnowledge;
    public GuessKnowledge GuessKnowledge
    {
        get
        {
            if (_guessKnowledge == null)
                _guessKnowledge = new GuessKnowledge(WordLength);
            return _guessKnowledge;
        }
    }

    private Word? Answer
    {
        get => DailyAnswers.Count > 0 ? DailyAnswers[0] : null;
    }

    private List<Word> _dailyAnswers;

    private List<Word> DailyAnswers
    {
        get
        {
            if (_dailyAnswers == null)
                _dailyAnswers = new List<Word>();
            return _dailyAnswers;
        }
    }

    private void GenerateDailyAnswers()
    {
        DailyAnswers.Clear();
        UnityEngine.Random.InitState(DateTime.Today.GetHashCode());
        DailyAnswers.Add("BLITZ");
        GuessKnowledge.SetAnswer(DailyAnswers[0]);
        KeyboardKnowledge.SetAnswer(DailyAnswers[0]);
        for (var i = 0; i < DailyAnswerCount; i++)
        {
            DailyAnswers.Add(_dictionary.GetRandomWord(WordLength));
            Debug.LogFormat("{0}: {1}", i, DailyAnswers[i + 1]);
        }
    }
    public override IEnumerator Initialize()
    {
        yield break;
    }

    public void RegisterDictionary(DictionaryManager dictionary)
    {
        _dictionary = dictionary;
        GenerateDailyAnswers();
    }

    public void UpdateKnowledge(Word guess)
    {
        KeyboardKnowledge.UpdateKnowledge(guess);
        GuessKnowledge.UpdateKnowledge(guess);
    }

    public AnnotatedWord Annotate(Word guess)
    {
        return GuessKnowledge.Annotate(guess);
    }

    public LetterKnowledge GlobalKnowledge(char c)
    {
        return KeyboardKnowledge[c];
    }

    public bool Correct(Word guess)
    {
        if (guess == Answer)
            return true;
        if (DailyAnswers.Contains(guess))
            DailyAnswers.RemoveAt(DailyAnswers.IndexOf(guess));
        return false;
    }

    public void NewProblem()
    {
        DailyAnswers.RemoveAt(0);
        GuessKnowledge.SetAnswer(DailyAnswers[0]);
        KeyboardKnowledge.SetAnswer(DailyAnswers[0]);
    }

    public Word SpoilAnswer()
    {
        return Answer.Value;
    }

    public override void ResetManager()
    {
        GenerateDailyAnswers();
    }


    public bool IsGameOver => DailyAnswers.Count == 0;
}
