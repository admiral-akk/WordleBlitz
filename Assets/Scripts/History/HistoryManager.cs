using System.Collections.Generic;

public class HistoryData : BaseRenderData<HistoryData> {
    public List<AnnotatedWord> Guesses { get; private set; }

    public void AddGuess(AnnotatedWord guess) {
        Guesses.Add(guess);
        HasUpdate = true;
    }

    public HistoryData() : base() {
        Guesses = new List<AnnotatedWord>();
    }
}

public class HistoryManager : RendererManager<HistoryData>, IUpdateObserver<GuessAnnotated> {

    private HistoryData _data;
    protected override HistoryData Data => _data ??= new HistoryData();

    public List<AnnotatedWord> GetHistory() {
        return new List<AnnotatedWord>(Data.Guesses);
    }

    public void Handle(GuessAnnotated update) {
        Data.AddGuess(update.AnnotatedGuess);
    }
}
