using UnityEngine;

public abstract class NewBaseRenderer<UpdateType> : MonoBehaviour, IUpdateObserver<UpdateType> 
    where UpdateType  : BaseUpdate<UpdateType>{
    public abstract void Handle(UpdateType update);
}
