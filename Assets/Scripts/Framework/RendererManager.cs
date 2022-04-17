using UnityEngine;

public abstract class RendererManager<DataType> : MonoBehaviour {
    [SerializeField] private BaseRenderer<DataType> Renderer;
    protected abstract DataType Data { get; }
    protected virtual bool HasRenderUpdate() {
        return true;
    }

    private void Update() {
        if (HasRenderUpdate())
            Renderer.Render(Data);
    }
}

public abstract class BaseRenderer<DataType> : MonoBehaviour {
    public abstract void Render(DataType data);
}