using UnityEngine;

public readonly struct ColorPalette
{
    public readonly Color Text;
    public readonly Color Correct;
    public readonly Color Wrong;
    public readonly Color Default;
    public readonly Color None;

    public ColorPalette(Color text, Color correct, Color wrong, Color defaultColor, Color none)
    {
        Text = text;
        Correct = correct;
        Wrong = wrong;
        Default = defaultColor;
        None = none;
    }
}
