using UnityEngine.UI;

public class TutorialRenderer : BaseRenderer<TutorialData> {
    private Graphic[] Graphics => GetComponentsInChildren<Graphic>();
    protected override void Render(TutorialData data) {
        switch (data.S) {
            case TutorialData.State.Start:
            case TutorialData.State.Open:
                foreach (var graphic in Graphics)
                    graphic.enabled = true;
                break;
            case TutorialData.State.Closed:
                foreach (var graphic in Graphics)
                    graphic.enabled = false;
                break;
        }
    }
}
