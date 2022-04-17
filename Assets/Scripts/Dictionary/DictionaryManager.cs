using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public abstract class DictionaryManager<LexiconUpdateType> : MonoBehaviour 
    where LexiconUpdateType : BaseUpdate<LexiconUpdateType> {
    [SerializeField] private string FileName;

    private string FilePath => Application.streamingAssetsPath + "/" + FileName;

    protected abstract LexiconUpdateType GenerateLexicon(List<WordDictionary> words);

    protected IEnumerator ReadLexiconFile() {
        var temp = new List<WordDictionary>();
        yield return FillWordCollection(FilePath, temp);
        GenerateLexicon(temp).Emit(gameObject);
    }
    private static void AddWord(Word word, List<WordDictionary> collection)
    {
        if (word.Length == 0)
            return;
        while (collection.Count < word.Length)
            collection.Add(new WordDictionary());
        collection[word.Length - 1].AddWord(word);
    }

    private static IEnumerator FillWordCollectionViaWeb(string url, List<WordDictionary> collection)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
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

    private void Awake() {
        StartCoroutine(ReadLexiconFile());
    }
}
