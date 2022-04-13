using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptRenderer : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField, Range(0.5f, 5)] private float Duration;

    private enum State
    {
        None,
        Hidden,
        InvalidWord,
        TooShort,
        ReusedWord,
        NonBlitz,
    }

    private void Awake()
    {
        S = State.Hidden;
    }

    private State _s;
    private State S
    {
        get => _s;
        set
        {
            _s = value;
            switch (_s)
            {
                case State.None:
                case State.Hidden:
                    border.gameObject.SetActive(false);
                    return;
                case State.InvalidWord:
                    text.text = "Word not in dictionary!";
                    break;
                case State.TooShort:
                    text.text = "Word has too few characters!";
                    break;
                case State.ReusedWord:
                    text.text = "Word used previously!";
                    break;
                case State.NonBlitz:
                    text.text = "Guess 'BLITZ' to start!";
                    break;
            }
            StartCoroutine(FadeoutCoroutine());
        }
    }

    private Graphic[] _graphics => new Graphic[]
    {
        text,border,background
    };

    private IEnumerator FadeoutCoroutine()
    {
        border.gameObject.SetActive(true);

        Pop.AddAnimation(border.gameObject, new PopParameters(0.2f, 0.3f));
        Fadeout.AddAnimation(border.gameObject, new FadeoutParameters(Duration, Duration / 2, _graphics));
        yield return new WaitForSeconds(Duration);
        S = State.Hidden;
    }

    public void HandleError(GuessResult.State s)
    {
        switch (s)
        {
            case GuessResult.State.InvalidWord:
                S = State.InvalidWord;
                break;
            case GuessResult.State.TooShort:
                S = State.TooShort;
                break;
            case GuessResult.State.ReusedWord:
                S = State.ReusedWord;
                break;
            case GuessResult.State.NonBlitz:
                S = State.NonBlitz;
                break;
        }
    }
}