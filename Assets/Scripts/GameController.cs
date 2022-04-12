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
    [SerializeField] private KnowledgeManager Knowledge;
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
        yield return InitializeManager(Knowledge);
        RegisterManagers();
        // Controller is finished.
        _waitingOnManagers--;
    }

    private void RegisterManagers()
    {
        Knowledge.RegisterDictionary(Dictionary);
        Guess.RegisterDictionary(Dictionary);
        Guess.RegisterKnowledge(Knowledge);
    }
    #endregion

    private bool Initialized => _waitingOnManagers == 0;

    private void FixedUpdate()
    {
        if (!Initialized)
            return;
        if (!Input.HasInput)
            return;
        var guess = Guess.HandleInput(Input.GetAndClearInput());
        if (guess == null)
            return;
        var annotatedGuess = Knowledge.Annotate(guess.Value);
        Knowledge.UpdateKnowledge(annotatedGuess.Word);
        History.GuessSubmitted(annotatedGuess);
        Input.UpdateKnowledge(Knowledge);
    }
}
