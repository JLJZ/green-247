using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Location : MonoBehaviour
{
    [field: SerializeField] public string DisplayName { get; private set; }
    [SerializeField] List<GameObject> AnimalPrefabs = new();

    [HideInInspector, SerializeField] List<Animal> _animals = new();
    public IReadOnlyList<Animal> Animals => _animals;

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

        _animals = AnimalPrefabs.ConvertAll(animal => animal.GetComponent<Animal>());
    }

    void Awake()
    {
        if (!SpriteRenderer)
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Assert.IsFalse(string.IsNullOrEmpty(DisplayName));
    }
}
