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
            Assert.AreEqual(LetterKnowledge.NoMoreInWord, annotatedGuess.Knowledge[i]);
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
            Assert.AreEqual(LetterKnowledge.CouldBeHere, annotatedGuess.Knowledge[i]);
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
        knowledge.SetAnswer("MERRY");
        knowledge.UpdateKnowledge("MUMMY");

        var guess = "MUMMY";
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            switch (i)
            {
                default:
                    break;
                case 0:
                case 4:
                    Assert.AreEqual(LetterKnowledge.Here, annotatedGuess.Knowledge[i]);
                    break;
                case 1:
                case 2:
                case 3:
                    Assert.AreEqual(LetterKnowledge.NoMoreInWord, annotatedGuess.Knowledge[i]);
                    break;
            }
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
            switch(i)
            {
                default:
                    break;
                case 0:
                    Assert.AreEqual(LetterKnowledge.Here, annotatedGuess.Knowledge[i], i.ToString());
                    break;
                case 2:
                case 3:
                    Assert.AreEqual(LetterKnowledge.CouldBeHere, annotatedGuess.Knowledge[i], i.ToString());
                    break;
                case 1:
                case 4:
                    Assert.AreEqual(LetterKnowledge.NoMoreInWord, annotatedGuess.Knowledge[i], i.ToString());
                    break;
            }
        }
    }

    [Test]
    public void DoubleLetter()
    {
        var wordLength = 5;
        var knowledge = new GuessKnowledge(wordLength);
        knowledge.SetAnswer("ABLES");
        knowledge.UpdateKnowledge("LLAMA");

        var guess = "LABEL";
        var annotatedGuess = knowledge.Annotate(guess);

        Assert.AreEqual(guess, annotatedGuess.Word);
        Assert.AreEqual(guess.Length, annotatedGuess.Knowledge.Length);
        for (var i = 0; i < guess.Length; i++)
        {
            switch (i)
            {
                default:
                    break;
                case 2:
                case 3:
                    Assert.AreEqual(LetterKnowledge.NoKnowledge, annotatedGuess.Knowledge[i], i.ToString());
                    break;
                case 0:
                case 1:
                    Assert.AreEqual(LetterKnowledge.CouldBeHere, annotatedGuess.Knowledge[i], i.ToString());
                    break;
                case 4:
                    Assert.AreEqual(LetterKnowledge.NoMoreInWord, annotatedGuess.Knowledge[i], i.ToString());
                    break;
            }
        }
    }
    #endregion
}
