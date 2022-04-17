using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseUpdateProducer<UpdateType> : BaseManager {

    private List<IUpdateObserver<UpdateType>> _observers;
    private List<IUpdateObserver<UpdateType>> Observers => _observers ??= new List<IUpdateObserver<UpdateType>>();

    protected void Emit(UpdateType update) {
        foreach (var observer in Observers)
            observer.Handle(update);
    }

    private void Awake() {
        var observers = GetComponents<MonoBehaviour>().OfType<IUpdateObserver<UpdateType>>();
        foreach (var observer in observers)
            Observers.Add(observer);
    }
}

public abstract class NewBaseManager<DataType, UpdateType> : BaseUpdateProducer<UpdateType> 
    where DataType : NewBaseData<UpdateType>
{
    protected abstract DataType Data { get; }
    protected void UpdateData(UpdateType update) {
        Data.Handle(update);
        Emit(update);
    }
}
