using NUnit.Framework;
using System;
using static CharacterKnowledge;

public class WordLevelKnowledgeTest
{
    #region Simple Cases
    [Test]
    public void EmptyKnowledge()
    {
        var knowledge = new CharacterKnowledge();

        foreach (var c in Language.Alphabet)
            Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
    }

    [Test]
    public void SetValueKnowledge()
    {
        foreach (LetterKnowledge k in Enum.GetValues(typeof(LetterKnowledge)))
        {
            var knowledge = new CharacterKnowledge();

            knowledge['A'] = k;

            foreach (var c in Language.Alphabet)
            {
                switch (c)
                {
                    default:
                        Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
                        break;
                    case 'A':
                        Assert.AreEqual(k, knowledge[c]);
                        break;
                }
            }
        }
    }

    #endregion

    #region Overrides
    [Test]
    public void NotHereOverridesSomeKnowledge()
    {
        foreach (LetterKnowledge k in Enum.GetValues(typeof(LetterKnowledge)))
        {
            var knowledge = new CharacterKnowledge();

            knowledge['A'] = k;
            knowledge['A'] = LetterKnowledge.NotHere;

            foreach (var c in Language.Alphabet)
            {
                switch (c)
                {
                    default:
                        Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
                        break;
                    case 'A':
                        if (k == LetterKnowledge.NoKnowledge || k == LetterKnowledge.CouldBeHere)
                        {
                            Assert.AreEqual(LetterKnowledge.NotHere, knowledge[c]);
                        } else
                        {
                            Assert.AreEqual(k, knowledge[c]);
                        }
                        break;
                }
            }
        }
    }

    [Test]
    public void HereOverridesSomeKnowledge()
    {
        foreach (LetterKnowledge k in Enum.GetValues(typeof(LetterKnowledge)))
        {
            var knowledge = new CharacterKnowledge();

            knowledge['A'] = k;
            knowledge['A'] = LetterKnowledge.Here;

            foreach (var c in Language.Alphabet)
            {
                switch (c)
                {
                    default:
                        Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
                        break;
                    case 'A':
                        if (k == LetterKnowledge.NoKnowledge || k == LetterKnowledge.CouldBeHere)
                        {
                            Assert.AreEqual(LetterKnowledge.Here, knowledge[c]);
                        }
                        else
                        {
                            Assert.AreEqual(k, knowledge[c]);
                        }
                        break;
                }
            }
        }
    }

    [Test]
    public void NotInWordOverridesSomeKnowledge()
    {
        foreach (LetterKnowledge k in Enum.GetValues(typeof(LetterKnowledge)))
        {
            var knowledge = new CharacterKnowledge();

            knowledge['A'] = k;
            knowledge['A'] = LetterKnowledge.NotInWord;

            foreach (var c in Language.Alphabet)
            {
                switch (c)
                {
                    default:
                        Assert.AreEqual(LetterKnowledge.NoKnowledge, knowledge[c]);
                        break;
                    case 'A':
                        if (k == LetterKnowledge.NoKnowledge || k == LetterKnowledge.CouldBeHere)
                        {
                            Assert.AreEqual(LetterKnowledge.NotInWord, knowledge[c]);
                        }
                        else
                        {
                            Assert.AreEqual(k, knowledge[c]);
                        }
                        break;
                }
            }
        }
    }
    #endregion
}
