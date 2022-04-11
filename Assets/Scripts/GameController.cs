using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private InputManager Input;
    [SerializeField] private GuessManager Guess;
    [SerializeField] private DictionaryManager Dictionary;
    [SerializeField] private HistoryManager History;
    private void Awake()
    {
        _waitingOnManagers = 0;
        InitializeManager(Input);
        InitializeManager(Guess);
        InitializeManager(Dictionary);
    }

    private int _waitingOnManagers;
    private bool Initialized => _waitingOnManagers == 0;

    private void InitializeManager(BaseManager manager)
    {
        _waitingOnManagers++;
        StartCoroutine(Handle(manager));
    }
    private IEnumerator Handle(BaseManager manager)
    {
        yield return manager.Initialize();
        _waitingOnManagers--;
    }

    private void FixedUpdate()
    {
        if (!Initialized)
            return;
        if (!Input.HasInput)
            return;
        Guess.AddChar(Input.GetAndClearInput());
    }
}
