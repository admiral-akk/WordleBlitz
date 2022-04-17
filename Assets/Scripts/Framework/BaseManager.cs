using System.Collections;
using UnityEngine;

public abstract class BaseManager : MonoBehaviour
{
    public virtual IEnumerator Initialize()
    {
        yield break;
    }

    public virtual void ResetManager() { }
}