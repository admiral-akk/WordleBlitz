using TMPro;
using UnityEngine;

public class ScoreboardRenderer : BaseRenderer<ScoreboardData> {
    [SerializeField] private TextMeshProUGUI text;
    protected override void Render(ScoreboardData data) {
        text.text = "";
        for (var i = 0; i < data.Count; i++) {
            text.text += string.Format("{0}. {1}\n", i + 1, (string)data[i]);
        }
    }
}
