using System.Diagnostics.Contracts;

public interface IUpdateObserver<UpdateType> {
    public void Handle(UpdateType update);
}

public interface IUpdateGenerator<InputUpdateType, OutputUpdateType> : IUpdateObserver<OutputUpdateType> {
    [Pure]
    public OutputUpdateType Generate(InputUpdateType update);
}
public abstract class NewBaseData<UpdateType> : IUpdateObserver<UpdateType> {
    public abstract void Handle(UpdateType update);
}
