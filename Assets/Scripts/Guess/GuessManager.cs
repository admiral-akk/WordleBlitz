using UnityEngine;

public class GuessManager : BaseManager
{
    [SerializeField] private GuessRenderer Renderer;

    public void AddChar(char c)
    {
        Renderer.AddChar(c);
    }
}
