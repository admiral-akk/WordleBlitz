using System.Collections.Generic;

public class ScoreboardManager : RendererManager<List<Word>>,
    IUpdateObserver<GuessAnnotated>,
    IUpdateObserver<KnowledgeInitialized> {

    private List<Word> _correct;
    private List<Word> Correct => _correct ??= new List<Word>();
    private int AnswersCount {
        set {
            Correct.Clear();
            while (value-- > 0)
                Correct.Add("");
        }
    }
    protected override List<Word> Data => _correct;
    public void Handle(GuessAnnotated update) {
        if (update.AnswerIndex == -1)
            return;
        _correct[update.AnswerIndex] = update.AnnotatedGuess.Word;
    }

    public void HandleCorrectGuess(AnnotatedWord correctWord, int index) {
        _correct[index] = correctWord.Word;
    }

    public void Handle(KnowledgeInitialized update) {
        AnswersCount = update.Knowledge.AnswerCount;
    }
}
