using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseManager
{
    [SerializeField] private KeyboardRenderer Keyboard;

    private KnowledgeManager _knowledge;

    private List<KeyRenderer> _keys;
    private List<KeyRenderer> Keys
    {
        get
        {
            if (_keys == null)
                _keys = new List<KeyRenderer>();
            return _keys;
        }
    }

    private void InitalizeKeyRenderer(KeyRenderer key, char c)
    {
        Keys.Add(key);
        key.Key = c;
        key.Callback = () => HitKey(c);
    }

    public override IEnumerator Initialize()
    {
        foreach (char c in Language.Alphabet)
            InitalizeKeyRenderer(Keyboard.AddKey(c), c);
        yield break;
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

    public void RegisterKnowledge(KnowledgeManager knowledge)
    {
        _knowledge = knowledge;
    }
}
