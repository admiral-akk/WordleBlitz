using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuessRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI guessText;

    public void UpdateGuess(Word guess)
    {
        guessText.text = (string)guess;
    }
}
