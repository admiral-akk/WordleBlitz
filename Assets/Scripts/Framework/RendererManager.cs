using UnityEngine;

public abstract class BaseRenderData<DataType>
    where DataType : BaseRenderData<DataType> {
    public bool HasUpdate {
        get;
        protected set;
    }

    public void RenderingComplete() => HasUpdate = false;

    public BaseRenderData() {
        HasUpdate = true;
    }
}

public abstract class RendererManager<DataType> : MonoBehaviour
    where DataType : BaseRenderData<DataType> {
    [SerializeField] private BaseRenderer<DataType> Renderer;
    protected abstract DataType Data { get; }

    private void Update() {
        if (Data.HasUpdate)
            Renderer.HandleRender(Data);
    }
}

public abstract class BaseRenderer<DataType> : MonoBehaviour
    where DataType : BaseRenderData<DataType> {
    protected abstract void Render(DataType data);
    public void HandleRender(DataType data) {
        Render(data);
        data.RenderingComplete();
    }
}