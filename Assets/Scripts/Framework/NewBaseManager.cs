using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class NewBaseManager<UpdateType> : BaseManager
    where UpdateType : NewUpdateType
{
    protected abstract NewBaseData<UpdateType> Data { get; }

    private List<IUpdateConsumer<UpdateType>> _observers;
    private List<IUpdateConsumer<UpdateType>> Observers
    {
        get
        {
            if (_observers == null)
                _observers = new List<IUpdateConsumer<UpdateType>>();
            return _observers;
        }
    }

    private void RegisterObservers(List<MonoBehaviour> components)
    {
        foreach (var observer in components.OfType<IUpdateConsumer<UpdateType>>())
            Observers.Add(observer);
    }
}
