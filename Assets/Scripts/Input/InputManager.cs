using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseManager
{
    [SerializeField] private KeyboardRenderer Keyboard;

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

    public bool HasInput => _currentInput.HasValue;
    public char GetAndClearInput()
    {
        var c = _currentInput.Value;
        _currentInput = null;
        return c;
    }

    private char? _currentInput;
   
    private void HitKey(char c)
    {
        _currentInput = c;
    }
    private void OnGUI()
    {
        var e = Event.current;
        if (e == null)
            return;
        if (e.type != EventType.KeyDown)
            return;
        if (!Language.IsAlpha((char)e.keyCode))
            return;
        HitKey((char)e.keyCode);
    }
}
