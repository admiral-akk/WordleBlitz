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
    [SerializeField] private TimerManager Timer;
    [SerializeField] private EndGameManager EndGame;
    [SerializeField] private PromptManager Prompt;
    [SerializeField] private ScoreboardManager Score;
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
        yield return InitializeManager(Timer);
        yield return InitializeManager(EndGame); 
        yield return InitializeManager(Prompt);
        yield return InitializeManager(Score);
        RegisterManagers();
        // Controller is finished.
        _waitingOnManagers--;
    }

    private void ResetGame()
    {
        // Controller is starting.
        _waitingOnManagers = 1;
        Guess.ResetManager();
        Input.ResetManager();
        Dictionary.ResetManager();
        History.ResetManager();
        Knowledge.ResetManager();
        Timer.ResetManager();
        EndGame.ResetManager();
        Prompt.ResetManager();
        Score.ResetManager();
        RegisterManagers();
        // Controller is finished.
        _waitingOnManagers--;
    }

    private void RegisterManagers()
    {
        Knowledge.RegisterDictionary(Dictionary);
        Guess.RegisterDictionary(Dictionary);
        Guess.RegisterKnowledge(Knowledge);
        Input.UpdateKnowledge(Knowledge);
    }
    #endregion

    private bool Initialized => _waitingOnManagers == 0;

    private void FixedUpdate()
    {
        if (!Initialized)
            return;
        var input = Input.GetAndClearInput();
        switch (input.InputType)
        {
            case PlayerInput.Type.NewGame:
                ResetGame();
                return;
        }
        if (Timer.TimeLeft < 0f)
        {
            EndGame.GameOver(History.GetCorrectGuesses(), Knowledge.SpoilAnswer());
            return;
        }
        Timer.DecrementTime(Time.fixedDeltaTime);
        var guess = Guess.HandleInput(input);
        if (guess.S == GuessResult.State.None)
            return;
        Prompt.HandleError(guess.S);
        if (guess.S != GuessResult.State.Valid)
            return;
        Knowledge.UpdateKnowledge(guess.Guess);
        Dictionary.Guess(guess.Guess);
        var annotatedGuess = Knowledge.Annotate(guess.Guess);
        Timer.GuessSubmitted(annotatedGuess);
        History.GuessSubmitted(annotatedGuess);
        Timer.DecrementTime(Time.fixedDeltaTime);
        Score.HandleGuess(annotatedGuess);
        if (Knowledge.Correct(guess.Guess))
        {
            Knowledge.NewProblem();
        }
        Input.UpdateKnowledge(Knowledge);
    }
}
