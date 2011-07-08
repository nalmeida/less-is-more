[System.Diagnostics.DebuggerStepThrough]
public sealed class Singleton<T> where T: class, new()
{

    static readonly T _instancia = new T();
    public static T Instancia
    {
        get { return _instancia; }
    }
}