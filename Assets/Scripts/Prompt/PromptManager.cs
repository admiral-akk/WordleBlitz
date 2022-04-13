using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptManager : BaseManager
{
    [SerializeField] private PromptRenderer Renderer;

    public override void ResetManager()
    {

    }

    public void HandleError(GuessResult.State s)
    {
        Renderer.HandleError(s);
    }
}
