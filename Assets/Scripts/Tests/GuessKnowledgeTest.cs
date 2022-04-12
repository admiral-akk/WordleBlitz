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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.NoKnowledge, annotatedGuess.Knowledge[i]);
        }
    }

    [Test]
    public void EmptyKnowledgePartialGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");

        var guess = "BLI";
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.NoKnowledge, annotatedGuess.Knowledge[i]);
        }
    }

    [Test]
    public void EmptyKnowledgeCompleteGuess()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");

        var guess = "BLITZ";
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.NoKnowledge, annotatedGuess.Knowledge[i]);
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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.NoKnowledge, annotatedGuess.Knowledge[i]);
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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            if (i < 2)
                Assert.AreEqual(LetterKnowledge.Here, annotatedGuess.Knowledge[i]);
            else
                Assert.AreEqual(LetterKnowledge.NoKnowledge, annotatedGuess.Knowledge[i]);
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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            if (i < 2)
                Assert.AreEqual(LetterKnowledge.Here, annotatedGuess.Knowledge[i]);
            else
                Assert.AreEqual(LetterKnowledge.NoKnowledge, annotatedGuess.Knowledge[i]);
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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
             Assert.AreEqual(LetterKnowledge.NoKnowledge, annotatedGuess.Knowledge[i]);
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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            if (i < 3)
                Assert.AreEqual(LetterKnowledge.Here, annotatedGuess.Knowledge[i]);
            else
                Assert.AreEqual(LetterKnowledge.NoKnowledge, annotatedGuess.Knowledge[i]);
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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.Here, annotatedGuess.Knowledge[i]);
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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.NotInWord, annotatedGuess.Knowledge[i]);
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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.NotHere, annotatedGuess.Knowledge[i]);
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
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.CouldBeHere, annotatedGuess.Knowledge[i]);
        }
    }

    [Test]
    public void WrongAndCorrectPosition()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("ZBLIT");

        var guess = "BLITZ";
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.CouldBeHere, annotatedGuess.Knowledge[i]);
        }
    }

    [Test]
    public void CorrectAndWrongPosition()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("BLITZ");
        knowledge.UpdateKnowledge("BLUES");

        var guess = "BULLS";
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            Assert.AreEqual(LetterKnowledge.CouldBeHere, annotatedGuess.Knowledge[i]);
        }
    }
    #endregion
}
