using System.Collections.Generic;
using System.Diagnostics.Contracts;

public class AnswerGeneratorInitialized {
    public IAnswerGenerator Generator;
    public AnswerGeneratorInitialized(IAnswerGenerator generator) {
        Generator = generator;
    }
}

public interface IAnswerGenerator {
    [Pure]
    public Word GetAnswer(int length);
}

public class AnswerLexicon : IAnswerGenerator {

    private List<WordDictionary> _words;

    [Pure]
    public Word GetAnswer(int length) {
        if (length < 1 || length > _words.Count)
            throw new System.Exception(string.Format("Length {0} out of array bounds!", length));
        var newAnswer = _words[length - 1].GetRandomWord();
        return newAnswer;
    }

    public AnswerLexicon(List<WordDictionary> words) {
        _words = words;
    }
}

public class AnswerLexiconManager : DictionaryManager<AnswerGeneratorInitialized> {
    protected override AnswerGeneratorInitialized GenerateLexicon(List<WordDictionary> words) {
        return new AnswerGeneratorInitialized(new AnswerLexicon(words));
    }
}