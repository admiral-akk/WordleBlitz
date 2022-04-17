using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class NewBaseManager<DataType, UpdateType> : BaseManager where DataType : NewBaseData<UpdateType>
{
    protected abstract DataType Data { get; }

    private List<IUpdateObserver<UpdateType>> _observers;
    private List<IUpdateObserver<UpdateType>> Observers => _observers ??= new List<IUpdateObserver<UpdateType>>();

    protected void UpdateData(UpdateType update) {
        Data.HandleUpdate(update);
        foreach (var observer in Observers)
            observer.HandleUpdate(update);
    }

    private void Awake() {
        var observers = GetComponents<MonoBehaviour>().OfType<IUpdateObserver<UpdateType>>();
        foreach (var observer in observers)
            Observers.Add(observer);
    }
}
