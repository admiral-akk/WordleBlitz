

    public readonly struct PlayerInput
{
    public enum Type
    {
        None, 
        Enter,
        Delete,
        HitKey,
    }

    public readonly char Letter;
    public readonly Type InputType;

    private PlayerInput(Type type, char c = '0')
    {
        InputType = type;
        Letter = c;
    }

    public static PlayerInput Enter()
    {
        return new PlayerInput(Type.Enter);
    }

    public static PlayerInput Delete()
    {
        return new PlayerInput(Type.Delete);
    }

    public static PlayerInput HitKey(char c)
    {
        return new PlayerInput(Type.HitKey, c);
    }
}