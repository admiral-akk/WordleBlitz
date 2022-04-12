using NUnit.Framework;
using static CharacterKnowledge;

public class GuessKnowledgeTest
{
    #region Correct Answer
    [Test]
    public void EmptyKnowledgeEmptyGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");

        var guess = "";

        for (var i = 0; i < wordLength; i++)
        {
            Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge.Get(guess, i));
        }
    }

    [Test]
    public void EmptyKnowledgePartialGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");

        var guess = "BLI";

        for (var i = 0; i < wordLength; i++)
        {
            Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge.Get(guess, i));
        }
    }

    [Test]
    public void EmptyKnowledgeCompleteGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");

        var guess = "BLITZ";

        for (var i = 0; i < wordLength; i++)
        {
            Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge.Get(guess, i));
        }
    }

    [Test]
    public void PartialKnowledgeEmptyGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("BLOOD");

        var guess = "";

        for (var i = 0; i < wordLength; i++)
        {
            Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge.Get(guess, i));
        }
    }

    [Test]
    public void PartialKnowledgePartialGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("BLOOD");

        var guess = "BLI";

        for (var i = 0; i < wordLength; i++)
        {   if (i < 2)
                Assert.AreEqual(LetterKnowledge.Here, knowledge.Get(guess, i));
            else
                Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge.Get(guess, i));
        }
    }

    [Test]
    public void PartialKnowledgeCompleteGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("BLOOD");

        var guess = "BLITZ";

        for (var i = 0; i < wordLength; i++)
        {
            if (i < 2)
                Assert.AreEqual(LetterKnowledge.Here, knowledge.Get(guess, i));
            else
                Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge.Get(guess, i));
        }
    }

    [Test]
    public void CompleteKnowledgeEmptyGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("BLITZ");

        var guess = "";

        for (var i = 0; i < wordLength; i++)
        {
            Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge.Get(guess, i));
        }
    }

    [Test]
    public void CompleteKnowledgePartialGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("BLITZ");

        var guess = "BLI";

        for (var i = 0; i < wordLength; i++)
        {
            if (i < 3)
                Assert.AreEqual(LetterKnowledge.Here, knowledge.Get(guess, i));
            else
                Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge.Get(guess, i));
        }
    }

    [Test]
    public void CompleteKnowledgeCompleteGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("BLITZ");

        var guess = "BLITZ";

        for (var i = 0; i < wordLength; i++)
        {
            Assert.AreEqual(LetterKnowledge.Here, knowledge.Get(guess, i));
        }
    }
    #endregion

    #region Not In Word
    [Test]
    public void NotInWordKnowledge()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("ANODE");

        var guess = "DANEO";

        for (var i = 0; i < wordLength; i++)
        {
            Assert.AreEqual(LetterKnowledge.NotInWord, knowledge.Get(guess, i));
        }
    }
    #endregion
    #region Wrong Position

    [Test]
    public void WrongPositionKnowledgeNotHere()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("ZBLIT");

        var guess = "ZBLIT";

        for (var i = 0; i < wordLength; i++)
        {
            Assert.AreEqual(LetterKnowledge.NotHere, knowledge.Get(guess, i));
        }
    }

    [Test]
    public void WrongPositionKnowledgeCouldBeInWord()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("ZBLIT");

        var guess = "BLITZ";

        for (var i = 0; i < wordLength; i++)
        {
            Assert.AreEqual(LetterKnowledge.CouldBeHere, knowledge.Get(guess, i));
        }
    }
    #endregion
}
