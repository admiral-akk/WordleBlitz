using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUpdateObserver<UpdateType>
{
    public void HandleUpdate(UpdateType update);
}
public abstract class NewBaseData<UpdateType> : IUpdateObserver<UpdateType>
{
    private List<UpdateType> _updates;
    private List<UpdateType> Updates => _updates ??= new List<UpdateType>(); 
    public bool HasUpdates => _updates.Count > 0;
    protected void Queue(UpdateType update) => Updates.Add(update);
    public abstract void HandleUpdate(UpdateType update);
}
