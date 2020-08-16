#if UNITY_EDITOR
public interface IInitialization
{
    bool AutoInitializate { get; }
    void Initializate();
}
#endif