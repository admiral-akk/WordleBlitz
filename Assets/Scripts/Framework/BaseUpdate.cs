using UnityEngine;

public class BaseUpdate<UpdateType> where UpdateType : BaseUpdate<UpdateType> {
    public void Emit(GameObject target) {
        var observers = target.GetComponents<IUpdateObserver<UpdateType>>();
        foreach (var observer in observers)
            observer.Handle((UpdateType)this);
    }
}