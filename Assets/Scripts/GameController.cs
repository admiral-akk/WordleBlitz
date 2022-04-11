using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private InputManager Input;
    [SerializeField] private GuessManager Guess;

    private void Awake()
    {
        Input.Initialize();
        Guess.Initialize();
    }

    private void FixedUpdate()
    {
        if (!Input.HasInput)
            return;
        Guess.AddChar(Input.GetAndClearInput());
    }
}
