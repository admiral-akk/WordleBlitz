using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharacterKnowledge;

public class CommandKeyRenderer : BaseLetterRenderer
{
    [SerializeField] private Button button;

    public enum Command
    {
        None,
        Delete,
        Enter
    }

    private Command _c;
    public Command C
    {
        get => _c;
        set
        {
            _c = value;
            switch (_c)
            {
                case Command.None:
                    break;
                case Command.Delete:
                    Render("DEL",LetterKnowledge.NoKnowledge);
                    break;
                case Command.Enter:
                    Render("ENTER", LetterKnowledge.NoKnowledge);
                    break;
            }
        }
    }

    public Action Callback
    {
        set
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => value());
        }
    }
}
