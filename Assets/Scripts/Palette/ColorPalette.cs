using UnityEngine;

public readonly struct ColorPalette
{
    public readonly Color Text;
    public readonly Color Correct;
    public readonly Color WrongPosition;
    public readonly Color Wrong;
    public readonly Color Default;
    public readonly Color None;

    public ColorPalette(Color text, Color correct, Color differentPosition,Color wrong, Color defaultColor, Color none)
    {
        Text = text;
        Correct = correct;
        WrongPosition = differentPosition;
        Wrong = wrong;
        Default = defaultColor;
        None = none;
    }
}
