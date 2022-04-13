using static CharacterKnowledge;

public class LetterRenderer : BaseRenderer
{
    public void Set(char c, LetterKnowledge k)
    {
        Render(c.ToString(), k);
    }
    public void Clear()
    {
        Render("", LetterKnowledge.NoKnowledge);
    }

    public void Pop()
    {
        gameObject.AddComponent(typeof(Pop));
    }
}
