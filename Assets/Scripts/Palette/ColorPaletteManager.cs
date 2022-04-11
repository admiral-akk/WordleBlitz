using UnityEngine;

public class ColorPaletteManager : MonoBehaviour
{
    [SerializeField] private Color Text;
    [SerializeField] private Color Correct;
    [SerializeField] private Color DifferentPosition;
    [SerializeField] private Color Wrong;
    [SerializeField] private Color Default;
    [SerializeField] private Color None;

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

    public static ColorPalette ColorPalette
    {
        get
        {
            return new ColorPalette(Instance.Text, Instance.Correct, Instance.DifferentPosition,Instance.Wrong, Instance.Default, Instance.None);
        }
    }
}
