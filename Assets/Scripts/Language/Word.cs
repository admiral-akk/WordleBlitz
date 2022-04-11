
public readonly struct Word
{
    private readonly string _word;
    private Word(string word)
    {
        _word = word.ToUpper();
    }

    public override int GetHashCode()
    {
        return _word.GetHashCode();
    }

    public override string ToString()
    {
        return _word;
    }

    public override bool Equals(object obj)
    {
        var other = (Word)obj;
        return _word == other._word;
    }

    public static Word operator +(Word w, char c) =>  w._word + c.ToString();
    public static Word operator +( char c, Word w) => c.ToString() + w._word;

    public static Word operator +(string s, Word w) => s + w;
    public static Word operator +(Word w, string s) => w + s;
    public static Word operator +(Word w1, Word w2) => new Word(w1._word + w2._word);
    public static bool operator ==(Word w1, Word w2) => w1._word == w2._word;
    public static bool operator !=(Word w1, Word w2) => w1._word != w2._word;

    public static implicit operator Word(string s) => new Word(s);
    public static explicit operator string(Word w) => w._word;

    public char this[int i] => _word[i];
    public int Length => _word.Length;
}