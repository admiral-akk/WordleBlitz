using UnityEngine;
using static CharacterKnowledge;

public class ColorPaletteManager : MonoBehaviour
{
    [SerializeField] private Color TextColor;
    [SerializeField] private Color Here;
    [SerializeField] private Color CouldBeHere;
    [SerializeField] private Color NotHere;
    [SerializeField] private Color NotInWord;
    [SerializeField] private Color NoKnowledge;

    private static ColorPaletteManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public static Color GetTextColor()
    {
        return Instance.TextColor;
    }

    public static Color GetColor(LetterKnowledge k)
    {
            switch (k)
            {
                default:
                case LetterKnowledge.NoKnowledge:
                    return Instance.NoKnowledge;
                case LetterKnowledge.NotInWord:
                    return Instance.NotInWord;
                case LetterKnowledge.NotHere:
                    return Instance.NotHere;
                case LetterKnowledge.CouldBeHere:
                    return Instance.CouldBeHere;
                case LetterKnowledge.Here:
                    return Instance.Here;
            }
    }
}
