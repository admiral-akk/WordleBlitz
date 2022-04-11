using UnityEngine;

public class GuessManager : BaseManager
{
    [SerializeField] private GuessRenderer Renderer;
    public override void Initialize()
    {
    }

    public void AddChar(char c)
    {
        Renderer.AddChar(c);
    }
}
