using System.Collections.Generic;
using UnityEngine;
using static CommandKeyRenderer;

public class KeyboardManager : MonoBehaviour,
    IUpdateObserver<KnowledgeInitialized>,
    IUpdateObserver<GuessAnnotated>,
    IUpdateObserver<NewAnswer> {
    [SerializeField] private KeyboardRenderer Keyboard;

    private Dictionary<char, KeyRenderer> _keys;
    private Dictionary<char, KeyRenderer> Keys => _keys ??= new Dictionary<char, KeyRenderer>();

    private KnowledgeManager _knowledge;
    private void InitalizeKeyRenderer(KeyRenderer key, char c) {
        Keys.Add(c, key);
        key.Key = c;
        key.Callback = () => HitKey(c);
    }

    private void InitalizeCommandKeyRenderer(CommandKeyRenderer key, Command c) {
        key.C = c;
        switch (c) {
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

    private void Awake() {
        foreach (char c in Language.Alphabet)
            InitalizeKeyRenderer(Keyboard.AddKey(c), c);
        InitalizeCommandKeyRenderer(Keyboard.AddCommand(Command.Delete), Command.Delete);
        InitalizeCommandKeyRenderer(Keyboard.AddCommand(Command.Enter), Command.Enter);
    }

    private void HitKey(char c) {
        PlayerInput.HitKey(c).Emit(gameObject);
    }
    private void Delete() {
        PlayerInput.Delete().Emit(gameObject);
    }
    private void Enter() {
        PlayerInput.Enter().Emit(gameObject);
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
