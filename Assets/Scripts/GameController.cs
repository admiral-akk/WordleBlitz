using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour, 
    IUpdateObserver<AnswerGeneratorInitialized>, 
    IUpdateObserver<ValidLexiconInitialized>,
    IUpdateObserver<GuessEntered>,
    IUpdateObserver<GuessError> {
    #region Managers
    [SerializeField] private InputManager Input;
    [SerializeField] private GuessManager Guess;
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
        Guess.RegisterKnowledge(Knowledge);
        Score.RegisterKnowledge(Knowledge);
        Input.UpdateKnowledge(Knowledge);
    }
    #endregion
    private bool Initialized => _waitingOnManagers == 0;

    private void FixedUpdate()
    {
        if (!Initialized || dictionariesInitialized< 2)
            return;
        if (Knowledge.IsGameOver)
        {
            Timer.GameOver();
            EndGame.GameOver(Timer.TimeLeft, Knowledge.GuessesRequired, History.GetHistory());
        }
        return;
    }

    private int dictionariesInitialized = 0;

    public void Handle(AnswerGeneratorInitialized update) {
        dictionariesInitialized++;
    }

    public void Handle(ValidLexiconInitialized update) {
        dictionariesInitialized++;
    }

    public void Handle(GuessEntered update) {
        var guess = update.Guess;
        Knowledge.UpdateKnowledge(guess);
        var (answerIndex, annotatedGuess) = Knowledge.Annotate(guess);
        Timer.GuessSubmitted(annotatedGuess);
        History.GuessSubmitted(annotatedGuess);
        if (answerIndex >= 0)
            Score.HandleCorrectGuess(annotatedGuess, answerIndex);
        if (Knowledge.Correct(guess)) {
            Knowledge.NewProblem();
        }
        Input.UpdateKnowledge(Knowledge);
    }

    public void Handle(GuessError update) {
        switch (update.Type) {
            case GuessError.ErrorType.None:
                break;
            case GuessError.ErrorType.TooShort:
                Prompt.HandleError(GuessResult.State.TooShort);
                break;
            case GuessError.ErrorType.InvalidWord:
                Prompt.HandleError(GuessResult.State.InvalidWord);
                break;
            case GuessError.ErrorType.ReusedWord:
                Prompt.HandleError(GuessResult.State.ReusedWord);
                break;
            case GuessError.ErrorType.NonBlitz:
                Prompt.HandleError(GuessResult.State.NonBlitz);
                break;
        }
    }
}
