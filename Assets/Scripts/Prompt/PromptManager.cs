using UnityEngine;

public class PromptManager : BaseManager, IUpdateObserver<GuessError> {
    [SerializeField] private PromptRenderer Renderer;

    public void Handle(GuessError update) {
        switch (update.Type) {
            case GuessError.ErrorType.None:
                break;
            case GuessError.ErrorType.TooShort:
                Renderer.HandleError(GuessResult.State.TooShort);
                break;
            case GuessError.ErrorType.InvalidWord:
                Renderer.HandleError(GuessResult.State.InvalidWord);
                break;
            case GuessError.ErrorType.ReusedWord:
                Renderer.HandleError(GuessResult.State.ReusedWord);
                break;
            case GuessError.ErrorType.NonBlitz:
                Renderer.HandleError(GuessResult.State.NonBlitz);
                break;
        }
    }
}
