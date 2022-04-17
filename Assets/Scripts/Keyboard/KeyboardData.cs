using System.Collections.Generic;
using static CharacterKnowledge;

public readonly struct KeyboardData {
    public readonly Dictionary<char, LetterKnowledge> Knowledge;

    public KeyboardData(Dictionary<char, LetterKnowledge> knowledge) {
        Knowledge = knowledge;
    }
}