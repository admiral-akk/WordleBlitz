using System.Collections.Generic;
using UnityEngine;

public class KeyboardRowRenderer : MonoBehaviour
{
    private List<KeyRenderer> _keys;
    private List<KeyRenderer> Keys
    {
        get
        {
            if (_keys == null)
                _keys = new List<KeyRenderer>();
            return _keys;
        }
    }

    private static string QwertyOrdering = "0QWERTYUIOPASDFGHJKLZXCVBNM1";
    public static int KeyOrder(KeyRenderer key)
    {
        return QwertyOrdering.IndexOf(key.Key);
    }
    public static int KeyOrder(CommandKeyRenderer key)
    {
        return key.C == CommandKeyRenderer.Command.Enter ? 0 : 100;
    }


    private void UpdateIndex()
    {
        for (var i = 0; i < Keys.Count; i++)
        {
            Keys[i].transform.SetSiblingIndex(i);
        }
    }

    private void AddKeyToList(KeyRenderer key)
    {
        var keyOrder = KeyOrder(key);
        for (var i = 0; i < Keys.Count; i++)
        {
            if (KeyOrder(Keys[i]) < keyOrder)
                continue;
            Keys.Insert(i, key);
            return;
        }
        Keys.Add(key);
    }

    public void AddKey(CommandKeyRenderer key)
    {
        key.transform.SetSiblingIndex(KeyOrder(key));
    }

    public void AddKey(KeyRenderer key)
    {
        AddKeyToList(key);
        key.transform.SetParent(transform);
        UpdateIndex();
    }
}
