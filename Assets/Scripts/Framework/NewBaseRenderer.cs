using UnityEngine;

public abstract class NewBaseRenderer<DataUpdateType> : MonoBehaviour, IUpdateConsumer<DataUpdateType>
    where DataUpdateType : NewUpdateType
{
    public abstract void HandleUpdate(DataUpdateType update);
}
