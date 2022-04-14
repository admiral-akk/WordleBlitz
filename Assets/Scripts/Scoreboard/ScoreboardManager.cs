using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : BaseManager
{
    [SerializeField] private ScoreboardRenderer Renderer;

    private List<Word> _correct;
    private int AnswersCount
    {
        set {
            _correct = new List<Word>();
            while (value-- > 0)
                _correct.Add("");
            Renderer.Render(_correct);
        }

    }

    public void HandleCorrectGuess(AnnotatedWord correctWord, int index)
    {
        Debug.LogFormat("Array length: {0}", _correct.Capacity);
        Debug.LogFormat("{0}: {1}", index, correctWord.Word);
        _correct[index] = correctWord.Word;
        Renderer.Render(_correct);
    }


    public void RegisterKnowledge(KnowledgeManager knowledge)
    {
        AnswersCount = knowledge.AnswerCount;
    }

    public override void ResetManager()
    {
        _correct.Clear();
        Renderer.Render(_correct);
    }
}
