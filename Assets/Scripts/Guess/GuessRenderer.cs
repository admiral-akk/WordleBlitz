using UnityEngine;

public class GuessRenderer : MonoBehaviour
{
    [SerializeField] private WordRenderer word;

    public void UpdateGuess(WordKnowledge guess)
    {
        word.UpdateWord(guess);
    }
}
