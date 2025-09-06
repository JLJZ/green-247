using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class Location : MonoBehaviour
{
    [SerializeField] string DisplayName;
    [SerializeField] List<GameObject> AnimalPrefabs = new();

    public IReadOnlyList<Animal> Animals { get; private set; }

    public Sprite Sprite
    {
        get => SpriteRenderer ? SpriteRenderer.sprite : GetComponent<SpriteRenderer>().sprite;
        set
        {
            if (SpriteRenderer)
                SpriteRenderer.sprite = value;
            else
                GetComponent<SpriteRenderer>().sprite = value;
        }
    }

    SpriteRenderer SpriteRenderer;

    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animals = AnimalPrefabs.ConvertAll(prefab => prefab.GetComponent<Animal>());
        Assert.IsFalse(string.IsNullOrEmpty(DisplayName));
    }
}
