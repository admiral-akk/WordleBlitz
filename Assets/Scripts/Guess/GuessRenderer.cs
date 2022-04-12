using UnityEngine;

public class GuessRenderer : MonoBehaviour
{
    [SerializeField] private WordRenderer word;

    public void UpdateGuess(AnnotatedWord guess, int maxLength)
    {
        word.UpdateWord(guess, maxLength );
    }
}
