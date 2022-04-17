using System.Collections.Generic;
using System.Diagnostics.Contracts;

public class ValidLexiconInitialized : BaseUpdate<ValidLexiconInitialized> {
    public IWordValidator Validator;
    [Pure]
    public ValidLexiconInitialized(IWordValidator validator) {
        Validator = validator;
    }
}

public interface IWordValidator {
    [Pure]
    public bool Valid(Word guess);
}

public class ValidLexicon : IWordValidator {

    private List<WordDictionary> _words;

    [Pure]
    public bool Valid(Word guess) {
        var len = guess.Length;
        if (len == 0)
            return false;
        if (len > _words.Count)
            return false;
        return _words[len - 1].IsValidWord(guess);
    }

    public ValidLexicon(List<WordDictionary> words) {
        _words = words;
    }
}

public class WordLexiconManager : DictionaryManager<ValidLexiconInitialized> {
    protected override ValidLexiconInitialized GenerateLexicon(List<WordDictionary> words) =>
        new ValidLexiconInitialized(new ValidLexicon(words));
}