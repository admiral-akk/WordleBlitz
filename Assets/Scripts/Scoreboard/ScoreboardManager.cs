using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour,
    IUpdateObserver<GuessAnnotated>,
    IUpdateObserver<KnowledgeInitialized> {
    [SerializeField] private ScoreboardRenderer Renderer;

    private List<Word> _correct;
    private int AnswersCount {
        set {
            _correct = new List<Word>();
            while (value-- > 0)
                _correct.Add("");
            Renderer.Render(_correct);
        }
    }

    public void Handle(GuessAnnotated update) {
        if (update.AnswerIndex == -1)
            return;
        _correct[update.AnswerIndex] = update.AnnotatedGuess.Word;
        Renderer.Render(_correct);
    }

    public void HandleCorrectGuess(AnnotatedWord correctWord, int index) {
        _correct[index] = correctWord.Word;
        Renderer.Render(_correct);
    }

    public void Handle(KnowledgeInitialized update) {
        AnswersCount = update.Knowledge.AnswerCount;
    }
}
