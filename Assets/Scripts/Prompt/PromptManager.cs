using UnityEngine;

public class PromptData : BaseRenderData<PromptData> {
    private string _prompt;

    public string Prompt {
        get => _prompt;
        set {
            _prompt = value;
            HasUpdate = true;
        }
    }

    public PromptData() : base() {
        Prompt = "";
    }
}

public class PromptManager : RendererManager<PromptData>, IUpdateObserver<GuessError> {
    [SerializeField] private string InvalidWord;
    [SerializeField] private string ReusedWord;
    [SerializeField] private string TooShort;
    [SerializeField] private string NonBlitz;

    private PromptData _data;
    protected override PromptData Data {
        get {
            if (_data == null)
                _data = new PromptData();
            return _data;
        }
    }

    public void Handle(GuessError update) {
        switch (update.Type) {
            case GuessError.ErrorType.None:
                break;
            case GuessError.ErrorType.TooShort:
                Data.Prompt = TooShort;
                break;
            case GuessError.ErrorType.InvalidWord:
                Data.Prompt = InvalidWord;
                break;
            case GuessError.ErrorType.ReusedWord:
                Data.Prompt = ReusedWord;
                break;
            case GuessError.ErrorType.NonBlitz:
                Data.Prompt = NonBlitz;
                break;
        }
    }
}