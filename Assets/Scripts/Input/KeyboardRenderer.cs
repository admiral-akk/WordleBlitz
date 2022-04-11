using UnityEngine;

public class KeyboardRenderer : MonoBehaviour
{
    [SerializeField] private GameObject KeyPrefab;
    [SerializeField] private KeyboardRowRenderer[] KeyRows;

    private static string[] QwertyRows = { "QWERTYUIOP", "ASDFGHJKL", "ZXCVBNM" };
    private static int CharToRow(char c)
    {
        for (var i = 0; i < QwertyRows.Length; i++)
        {
            if (QwertyRows[i].Contains(c.ToString().ToUpper()))
            {
                return i;
            }
        }
        throw new System.Exception("Character not found in Qwerty rows!");
    }
    public KeyRenderer AddKey(char c)
    {
        var row = KeyRows[CharToRow(c)];
        var key = Instantiate(KeyPrefab).GetComponent<KeyRenderer>();
        key.Key = c;
        row.AddKey(key);
        return key;
    }
}
