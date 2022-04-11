using UnityEngine;

public class GuessRenderer : MonoBehaviour
{
    [SerializeField] private WordRenderer word;

    public void UpdateGuess(Word guess, int maxLength)
    {
        word.UpdateWord(guess, maxLength);
    }
}
