using UnityEngine;

public class BaseUpdate<UpdateType> where UpdateType : BaseUpdate<UpdateType> {
    public void Emit(GameObject target) {
        var observers = target.GetComponents<IUpdateObserver<UpdateType>>();
        foreach (var observer in observers)
            observer.Handle((UpdateType)this);
    }
}
public interface IUpdateObserver<UpdateType> where UpdateType : BaseUpdate<UpdateType> {
    // TODO: should change this to only handle data of update, so that we don't accidently re-emit it.
    public void Handle(UpdateType update);
}
