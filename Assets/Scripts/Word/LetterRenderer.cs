using static CharacterKnowledge;

public class LetterRenderer : BaseRenderer
{
    public void Set(char c, LetterKnowledge k)
    {
        Render(c.ToString(), k);
        gameObject.AddComponent(typeof(Pop));
    }
    public void Clear()
    {
        Render("", LetterKnowledge.NoKnowledge);
    }
}
