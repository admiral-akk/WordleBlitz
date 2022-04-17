using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseUpdateProducer<UpdateType> : BaseManager 
    where UpdateType : BaseUpdate<UpdateType> {
    protected void Emit(UpdateType update) {
        update.Emit(gameObject);
    }
}

public abstract class NewBaseManager<DataType, UpdateType> : BaseUpdateProducer<UpdateType> 
    where DataType : NewBaseData<UpdateType>
    where UpdateType : BaseUpdate<UpdateType>
{
    protected abstract DataType Data { get; }
    protected void UpdateData(UpdateType update) {
        Data.Handle(update);
        Emit(update);
    }
}
