public struct AutoConstruct<T> where T : new() {
    private T _data;
    public T Data => _data ??= new T(); 
}