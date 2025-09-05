using UnityEngine;

[DisallowMultipleComponent]
public class Service<T> : MonoBehaviour where T : Service<T>, new()
{
    private static readonly object _lock = new object();
    static public T Instance { get; private set; }

    protected virtual void Awake()
    {
        lock (_lock)
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
