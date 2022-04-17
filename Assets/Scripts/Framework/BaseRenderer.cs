using UnityEngine;

public abstract class RendererManager<DataType> : MonoBehaviour {
    [SerializeField] private readonly IRenderer<DataType> Renderer;
    protected abstract DataType Data { get; set; }
    protected abstract bool HasRenderUpdate { get; set; }

    private void Update() {
        if (HasRenderUpdate)
            Renderer.Render(Data);
    }
}

public interface IRenderer<DataType> {
    public abstract void Render(DataType data);
}