using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptRenderer : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField, Range(0.5f, 5)] private float Duration;


    private enum State
    {
        None,
        Hidden,
        InvalidWord,
        TooShort,
        ReusedWord
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
            Debug.Log(_s);
            switch (_s)
            {
                case State.None:
                case State.Hidden:
                    background.gameObject.SetActive(false);
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
            }
            StartCoroutine(Fadeout());
        }
    }

    private IEnumerator Fadeout()
    {
        background.gameObject.SetActive(true);
        background.gameObject.AddComponent(typeof(Pop));
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
        }
    }
}
