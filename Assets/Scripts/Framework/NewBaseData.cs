using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewUpdateType
{

}

public interface IUpdateConsumer<UpdateType> where UpdateType : NewUpdateType
{
    public void HandleUpdate(UpdateType update);
}
public class NewBaseData<UpdateType> where UpdateType : NewUpdateType
{
    private List<UpdateType> _updates;
    private List<UpdateType> Updates { get => _updates ??= new List<UpdateType>(); }
    public bool HasUpdates => _updates.Count > 0;

    protected void AddUpdate(UpdateType update)
    {
        Updates.Add(update);
    }

}
