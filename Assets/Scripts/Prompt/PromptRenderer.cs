using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptRenderer : BaseRenderer<PromptData> {
    [SerializeField] private Image background;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField, Range(0.5f, 5)] private float FadeDuration;
    [SerializeField, Range(0.1f, 0.5f)] private float PopDuration;
    [SerializeField, Range(0.1f, 0.5f)] private float PopMagnitude;

    protected override void Render(PromptData data) {
        if (data.Prompt.Length == 0) {
            SetEnabled(false);
            return;
        }
        Pop.AddAnimation(border.gameObject, new PopParameters(PopDuration, PopMagnitude));
        Fadeout.AddAnimation(border.gameObject, new FadeoutParameters(FadeDuration, _graphics));
        text.text = data.Prompt;
    }
    private Graphic[] _graphics => new Graphic[] {
        text,border,background
    };

    private void SetEnabled(bool isEnabled) {
        background.enabled = isEnabled;
        border.enabled = isEnabled;
        text.enabled = isEnabled;
    }

    private void Awake() {
        SetEnabled(false);
    }
}