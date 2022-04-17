using UnityEngine;

public abstract class NewBaseRenderer<UpdateType> : MonoBehaviour, IUpdateObserver<UpdateType>{
    public abstract void HandleUpdate(UpdateType update);
}
