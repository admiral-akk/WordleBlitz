using System.Collections.Generic;


public class ScoreboardData : BaseRenderData<ScoreboardData> {

    private List<Word> _correct;
    public Word this[int index] { get => _correct[index]; set {
            if (_correct[index] == value)
                return;
            _correct[index] = value;
            HasUpdate = true;
        }
    }
    public int Count => _correct.Count;

    public ScoreboardData(int answerCount) : base() {
        _correct = new List<Word>();
        for (var i = 0; i < answerCount; i++)
            _correct.Add("");
    }
}


public class ScoreboardManager : RendererManager<ScoreboardData>,
    IUpdateObserver<GuessAnnotated>,
    IUpdateObserver<KnowledgeInitialized> {

    private ScoreboardData _data;
    protected override ScoreboardData Data {
        get {
            if (_data == null)
                _data = new ScoreboardData(0);
            return _data;
        }
    }
    public void Handle(GuessAnnotated update) {
        if (update.AnswerIndex == -1)
            return;
        Data[update.AnswerIndex] = update.AnnotatedGuess.Word;
    }

    public void HandleCorrectGuess(AnnotatedWord correctWord, int index) {
        Data[index] = correctWord.Word;
    }

    public void Handle(KnowledgeInitialized update) {
        _data = new ScoreboardData(update.Knowledge.AnswerCount);
    }
}
