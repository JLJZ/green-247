using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Location : MonoBehaviour
{
    [SerializeField] string DisplayName;
    [SerializeField] List<GameObject> AnimalPrefabs = new();

    public IReadOnlyList<Animal> Animals { get; private set; }

    public Sprite Sprite
    {
        get => SpriteRenderer ? SpriteRenderer.sprite : GetComponentInChildren<SpriteRenderer>().sprite;
        set
        {
            if (SpriteRenderer)
                SpriteRenderer.sprite = value;
            else
                GetComponentInChildren<SpriteRenderer>().sprite = value;
        }
    }

    [SerializeField] SpriteRenderer SpriteRenderer;

    void OnValidate()
    {
        if (!SpriteRenderer)
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (!SpriteRenderer)
            {
                var instance = new GameObject("Sprite", typeof(SpriteRenderer));
                instance.transform.SetParent(transform);
                SpriteRenderer = instance.GetComponent<SpriteRenderer>();
            }
        }
        // Check unique prefabs
        for (int i = 0; i < AnimalPrefabs.Count; i++)
        {
            for (int j = i + 1; j < AnimalPrefabs.Count; j++)
            {
                if (AnimalPrefabs[i] == AnimalPrefabs[j])
                {
                    Debug.LogWarning("There are duplicate animals in the prefab list.", AnimalPrefabs[j]);
                    return;
                }
            }
        }
    }

    void Awake()
    {
        if (!SpriteRenderer)
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Animals = AnimalPrefabs.ConvertAll(prefab => prefab.GetComponent<Animal>());
        Assert.IsFalse(string.IsNullOrEmpty(DisplayName));
    }
}
