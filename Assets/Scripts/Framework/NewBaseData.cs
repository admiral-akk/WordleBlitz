
public interface IUpdateObserver<UpdateType> where UpdateType : BaseUpdate<UpdateType> {
    public void Handle(UpdateType update);
}

public abstract class NewBaseData<UpdateType> : IUpdateObserver<UpdateType> 
    where UpdateType : BaseUpdate<UpdateType> {
    public abstract void Handle(UpdateType update);
}
