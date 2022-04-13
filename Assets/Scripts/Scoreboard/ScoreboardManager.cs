using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : BaseManager
{
    [SerializeField] private ScoreboardRenderer Renderer;

    private List<Word> _correct;
    private List<Word> Correct
    {
        get {
            if (_correct == null)
                _correct = new List<Word>();
            return _correct;
        }
    }

    public void HandleGuess(AnnotatedWord word)
    {
        if (word.Correct && word.Word != "BLITZ")
        {
            Correct.Add(word.Word);
            Renderer.Render(Correct);
        }
    }
    public override void ResetManager()
    {
        Correct.Clear();
        Renderer.Render(Correct);
    }
}
