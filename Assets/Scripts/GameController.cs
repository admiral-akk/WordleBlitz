using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Managers
    [SerializeField] private InputManager Input;
    [SerializeField] private GuessManager Guess;
    [SerializeField] private DictionaryManager Dictionary;
    [SerializeField] private HistoryManager History;
    #endregion

    #region Initialization

    private int _waitingOnManagers;
    private void Awake()
    {
        StartCoroutine(InitializeManagers());
    }
    private IEnumerator InitializeManager(BaseManager manager)
    {
        _waitingOnManagers++;
        yield return manager.Initialize();
        _waitingOnManagers--;
    }

    private IEnumerator InitializeManagers()
    {
        // Controller is starting.
        _waitingOnManagers = 1;
        yield return InitializeManager(Input);
        yield return InitializeManager(Guess);
        yield return InitializeManager(Dictionary);
        yield return InitializeManager(History);
        RegisterManagers();
        // Controller is finished.
        _waitingOnManagers--;
    }

    private void RegisterManagers()
    {
        Guess.RegisterDictionary(Dictionary);
    }
    #endregion

    private bool Initialized => _waitingOnManagers == 0;

    private void FixedUpdate()
    {
        if (!Initialized)
            return;
        if (!Input.HasInput)
            return;
       var guessResult = Guess.AddChar(Input.GetAndClearInput());
        switch (guessResult.S)
        {
            default:
                return;
            case GuessResult.State.Wrong:
                History.GuessSubmitted(guessResult.Guess);
                return;
            case GuessResult.State.Correct:
                History.GuessSubmitted(guessResult.Guess);
                return;
        }
    }
}
