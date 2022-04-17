using UnityEngine;

public class PromptManager : BaseManager {
    [SerializeField] private PromptRenderer Renderer;

    public void HandleError(GuessResult.State s) {
        Renderer.HandleError(s);
    }
}
