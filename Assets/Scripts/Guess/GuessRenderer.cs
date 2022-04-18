using UnityEngine;

public class GuessRenderer : BaseRenderer<GuessData> {
    [SerializeField] private WordRenderer word;

    protected override void Render(GuessData data) {
        word.UpdateWord(data.Guess, data.MaxLength);
    }
}
