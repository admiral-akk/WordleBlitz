using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
    [SerializeField] private Graphic[] promptElements;

    private void Awake()
    {
        foreach (var element in promptElements)
        {
            element.enabled = false;
        }
    }

    public void PromptAnimation(float duration, float popMagnitude, float fadeDuration)
    {
        Pop.AddAnimation(gameObject, new PopParameters(duration, popMagnitude));
        Fadeout.AddAnimation(gameObject, new FadeoutParameters(fadeDuration, fadeDuration / 2, promptElements));
    }
}
