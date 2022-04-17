using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommandKeyRenderer;

public class InputManager : BaseManager, 
    IUpdateObserver<KnowledgeInitialized>, 
    IUpdateObserver<GuessAnnotated>,
    IUpdateObserver<NewAnswer>
{
    [SerializeField] private KeyboardRenderer Keyboard;

    private Dictionary<char, KeyRenderer> _keys;
    private Dictionary<char, KeyRenderer> Keys => _keys ??= new Dictionary<char, KeyRenderer>();

    private KnowledgeManager _knowledge;
    private void InitalizeKeyRenderer(KeyRenderer key, char c)
    {
        Keys.Add(c,key);
        key.Key = c;
        key.Callback = () => HitKey(c);
    }

    private void InitalizeCommandKeyRenderer(CommandKeyRenderer key, Command c)
    {
        key.C = c;
        switch (c)
        {
            case Command.None:
                break;
            case Command.Delete:
                key.Callback = () => Delete();
                break;
            case Command.Enter:
                key.Callback = () => Enter();
                break;
        }
    }
    public override IEnumerator Initialize()
    {
        foreach (char c in Language.Alphabet)
            InitalizeKeyRenderer(Keyboard.AddKey(c), c);
        InitalizeCommandKeyRenderer(Keyboard.AddCommand(Command.Delete), Command.Delete);
        InitalizeCommandKeyRenderer(Keyboard.AddCommand(Command.Enter), Command.Enter);
        yield break;
    }

    private void Awake() {
        StartCoroutine(Initialize());
    }

    public bool HasInput => _currentInput.InputType != PlayerInput.Type.None;
    public PlayerInput GetAndClearInput()
    {
        var c = _currentInput;
        _currentInput = new PlayerInput();
        return c;
    }

    private PlayerInput _currentInput;
   
    private void HitKey(char c)
    {
        _currentInput = PlayerInput.HitKey(c);
    }
    private void Delete()
    {
        _currentInput = PlayerInput.Delete();
    }
    private void Enter()
    {
        _currentInput = PlayerInput.Enter();
    }

    private void OnGUI()
    {
        var e = Event.current;
        if (e == null)
            return;
        if (e.type != EventType.KeyDown)
            return;
        if (Language.IsAlpha((char)e.keyCode))
        {
            HitKey((char)e.keyCode);
            return;
        }
        if (e.keyCode == KeyCode.Delete || e.keyCode == KeyCode.Backspace)
        {
            Delete();
        }
        if (e.keyCode == KeyCode.KeypadEnter || e.keyCode == KeyCode.Return)
        {
            Enter();
        }
    }

    public override void ResetManager() {
        var knowledge = GetComponent<KnowledgeManager>();
        foreach (var c in Language.Alphabet) {
            var k = knowledge.GlobalKnowledge(c);
            Keys[c].Knowledge = k;
        }
    }

   public void Handle(KnowledgeInitialized update) {
        _knowledge = update.Knowledge;
    }

    public void Handle(GuessAnnotated update) {
        foreach (var c in Language.Alphabet) {
            var k = _knowledge.GlobalKnowledge(c);
            Keys[c].Knowledge = k;
        }
    }

    public void Handle(NewAnswer update) {
        foreach (var c in Language.Alphabet) {
            var k = _knowledge.GlobalKnowledge(c);
            Keys[c].Knowledge = k;
        }
    }
}
