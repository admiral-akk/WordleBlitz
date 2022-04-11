using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DictionaryManager : BaseManager
{
    private List<WordDictionary> _words;
    private List<WordDictionary> _answers;

    private static string _wordPath = Application.streamingAssetsPath + "/GeneratedWords.txt";
    private static string _answerPath = Application.streamingAssetsPath + "/GeneratedAnswers.txt";

    public override IEnumerator Initialize()
    {
        _words = new List<WordDictionary>();
        _answers = new List<WordDictionary>();
        yield return FillWordCollection(_wordPath, _words);
        yield return FillWordCollection(_answerPath, _answers);
        Debug.Log("Dictionary loaded");
    }

    private static void AddWord(string word, List<WordDictionary> collection)
    {
        while (collection.Count < word.Length)
            collection.Add(new WordDictionary());
        collection[word.Length - 1].AddWord(word);
    }

    private static IEnumerator FillWordCollectionViaWeb(string url, List<WordDictionary> collection)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        foreach (var word in www.downloadHandler.text.Split('\n'))
        {
            AddWord(word, collection);
        }
    }
    private static void FillWordCollectionViaFile(string filePath, List<WordDictionary> collection)
    {
        foreach (var word in File.ReadLines(filePath))
        {
            AddWord(word, collection);
        }
    }

    private static IEnumerator FillWordCollection(string path, List<WordDictionary> collection)
    {
        if (path.Contains("http:") || path.Contains("https:"))
        {
            yield return FillWordCollectionViaWeb(path, collection);
        }
        else
        {
            FillWordCollectionViaFile(path, collection);
        }
    }

    public bool IsValidWord(Word guess)
    {
        var len = guess.Length;
        if (len == 0)
            return false;
        if (len > _words.Count)
            return false;
        return _words[len - 1].IsValidWord(guess);
    }


    public Word GetRandomWord(int length)
    {
        if (length < 1 || length > _answers.Count)
            throw new System.Exception(string.Format("Length {0} out of array bounds!", length));
        return _answers[length - 1].GetRandomWord();
    }

}
