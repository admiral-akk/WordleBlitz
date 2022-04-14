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

    private Dictionary<Word, int> _indices;

    private void GenerateDailyAnswers()
    {
        DailyAnswers.Clear();
        _indices = new Dictionary<Word, int>();
        UnityEngine.Random.InitState(DateTime.Today.GetHashCode());
        DailyAnswers.Add("BLITZ");
        GuessKnowledge.SetAnswer(DailyAnswers[0]);
        KeyboardKnowledge.SetAnswer(DailyAnswers[0]);
        for (var i = 0; i < DailyAnswerCount; i++)
        {
            var word = _dictionary.GetRandomWord(WordLength);
            DailyAnswers.Add(word);
            _indices[word] = i;
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

    public (int, AnnotatedWord) Annotate(Word guess)
    {
        var annotated = GuessKnowledge.Annotate(guess);
        return (_indices.ContainsKey(guess) ? _indices[guess] : -1, annotated);
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
        if (IsGameOver)
            return;
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


    public int AnswerCount => DailyAnswerCount;
    public bool IsGameOver => DailyAnswers.Count == 0;
}
