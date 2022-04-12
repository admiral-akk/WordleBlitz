using NUnit.Framework;
using static CharacterKnowledge;

public class KeyboardKnowledgeTest
{
    #region Simple Cases
    [Test]
    public void EmptyKnowledge()
    {
        var knowledge = new KeyboardKnowledge(5);
        knowledge.SetAnswer("BLITZ");

        foreach (var c in Language.Alphabet)
            Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
    }

    [Test]
    public void WrongGuess()
    {
        var knowledge = new KeyboardKnowledge(5);
        knowledge.SetAnswer("BLITZ");
        var guess = "ENEMA";
        knowledge.UpdateKnowledge(guess);

        foreach (var c in Language.Alphabet)
        {
            if (guess.Contains(c.ToString())) {
                Assert.AreEqual(LetterKnowledge.NotInWord, knowledge[c]);
            } else
            {
                Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
            }
        }
    }

    [Test]
    public void WrongPosition()
    {
        var knowledge = new KeyboardKnowledge(5);
        knowledge.SetAnswer("BLITZ");
        var guess = "LITZB";
        knowledge.UpdateKnowledge(guess);

        foreach (var c in Language.Alphabet)
        {
            if (guess.Contains(c.ToString()))
            {
                Assert.AreEqual(LetterKnowledge.CouldBeHere, knowledge[c]);
            }
            else
            {
                Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
            }
        }
    }

    [Test]
    public void Correct()
    {
        var knowledge = new KeyboardKnowledge(5);
        knowledge.SetAnswer("BLITZ");
        var guess = "BLITZ";
        knowledge.UpdateKnowledge(guess);

        foreach (var c in Language.Alphabet)
        {
            if (guess.Contains(c.ToString()))
            {
                Assert.AreEqual(LetterKnowledge.Here, knowledge[c]);
            }
            else
            {
                Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
            }
        }
    }
    #endregion

    #region Complex cases
    [Test]
    public void MultipleWrongPosition()
    {
        var knowledge = new KeyboardKnowledge(5);
        knowledge.SetAnswer("BLITZ");

        string[] guesses = { "LOWER", "WILDS" };

        foreach (var guess in guesses)
            knowledge.UpdateKnowledge(guess);

        foreach (var c in Language.Alphabet)
        {
            switch (c)
            {
                default:
                    Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
                    break;
                case 'L':
                case 'I':
                    Assert.AreEqual(LetterKnowledge.CouldBeHere, knowledge[c]);
                    break;
                case 'O':
                case 'E':
                case 'R':
                case 'W':
                case 'D':
                case 'S':
                    Assert.AreEqual(LetterKnowledge.NotInWord, knowledge[c]);
                    break;
            }
        }
    }

    [Test]
    public void PartiallyCorrect()
    {
        var knowledge = new KeyboardKnowledge(5);
        knowledge.SetAnswer("BLITZ");
        var guess = "BLOOD";
        knowledge.UpdateKnowledge(guess);

        foreach (var c in Language.Alphabet)
        {
            switch (c)
            {
                default:
                    Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
                    break;
                case 'B':
                case 'L':
                    Assert.AreEqual(LetterKnowledge.Here, knowledge[c]);
                    break;
                case 'O':
                case 'D':
                    Assert.AreEqual(LetterKnowledge.NotInWord, knowledge[c]);
                    break;
            }
        }
    }

    [Test]
    public void CorrectThenWrongPosition()
    {
        var knowledge = new KeyboardKnowledge(5);
        knowledge.SetAnswer("BLITZ");

        string[] guesses = { "BLOOD", "DRABS" };
        foreach (var guess in guesses)
            knowledge.UpdateKnowledge(guess);

        foreach (var c in Language.Alphabet)
        {
            switch (c)
            {
                default:
                    Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
                    break;
                case 'B':
                case 'L':
                    Assert.AreEqual(LetterKnowledge.Here, knowledge[c]);
                    break;
                case 'O':
                case 'D':
                case 'R':
                case 'A':
                case 'S':
                    Assert.AreEqual(LetterKnowledge.NotInWord, knowledge[c]);
                    break;
            }
        }
    }
    #endregion
}
