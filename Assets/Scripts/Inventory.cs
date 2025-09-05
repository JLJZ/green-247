using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Inventory<T> : ICollection<T> where T : Component
{
    public Lazy<IReadOnlyDictionary<string, T>> All { get; }

    [SerializeField] List<T> Contents = new List<T>();

    public Inventory(string resourcePath)
    {
        All = new(() => Resources.LoadAll(resourcePath, typeof(GameObject))
            .Cast<GameObject>()
            .ToDictionary(go => go.name, go => go.GetComponent<T>()));
    }

    public void Add(T item) => Contents.Add(FindPrefab(item));

    public bool Contains(T item) => Contents.Contains(FindPrefab(item));

    public bool Remove(T item) => Contents.Remove(FindPrefab(item));

    public void Clear() => Contents.Clear();

    public void CopyTo(T[] array, int arrayIndex) => Contents.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => Contents.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => Contents.Count;

    public bool IsReadOnly => false;

    T FindPrefab(T item)
    {
        if (All.Value.TryGetValue(item.name, out T prefab))
        {
            return prefab;
        }
        else
        {
            Debug.LogError($"Could not find item {item.name}");
            return null;
        }
    }
}
