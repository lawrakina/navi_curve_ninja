using UnityEngine;

public abstract class ComponentProvider<T> : MonoBehaviour where T : Component
{
    public abstract T[] Components { get; protected set; }

    public void Reset()
    {
        Components = GetComponentsInChildren<T>();
    }
}
