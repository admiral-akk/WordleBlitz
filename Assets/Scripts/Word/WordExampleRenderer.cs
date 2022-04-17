using UnityEngine;
using static CharacterKnowledge;

public class WordExampleRenderer : MonoBehaviour
{
    [SerializeField] private string Example;
    [SerializeField] private LetterKnowledge[] ExampleKnowledge;
    [SerializeField] private WordRenderer Renderer;

    private void Awake()
    {
        Renderer.UpdateWord(new AnnotatedWord(Example, ExampleKnowledge), Example.Length);
    }
}
