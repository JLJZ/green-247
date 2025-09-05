using System.Linq;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class AnimalSpawner : MonoBehaviour
{
    [field: SerializeField]
    Bounds Bounds { get; set; }
        = new Bounds(new Vector3(0, 0, 0), new Vector3(2.5f, 2.5f, 0));

    [SerializeField]
    List<GameObject> _animalPrefabs = new();

    [Min(1)]
    [field: SerializeField]
    int MaxAnimalCount { get; set; } = 5;

    public IReadOnlyList<GameObject> AnimalPrefabs => _animalPrefabs;

    void OnValidate()
    {
        foreach (var prefab in AnimalPrefabs)
        {
            Assert.IsTrue(prefab.TryGetComponent(out Animal animal),
                $"{prefab.name} must have an Animal component.");
            Assert.IsTrue(animal.WanderingBounds.size.x > 0 && animal.WanderingBounds.size.y > 0,
                $"{prefab.name} must have a non-zero WanderingBounds.");
        }
    }

    void Start()
    {
        _animalPrefabs = InventoryService.Instance.Animals
            .Select(animal => animal.gameObject)
            .ToList();
        OnValidate();
        SpawnAnimals(Random.Range(1, MaxAnimalCount));
    }

    public List<Animal> SpawnAnimals(int count)
    {
        Assert.IsTrue(count > 0 && count <= MaxAnimalCount,
            "Animal count must be between 1 and MaxAnimalCount.");

        var animalPrefabs = Enumerable.Range(0, AnimalPrefabs.Count)
            .OrderBy(_ => Random.Range(0f, 1f))
            .Take(count)
            .Select(AnimalPrefabs.ElementAt);

        var instances = new List<Animal>();
        foreach (var prefab in animalPrefabs)
        {
            var instance = Instantiate(prefab,
                ChooseRandomTargetPosition(Bounds, 0),
                quaternion.identity,
                transform);

            var animal = instance.GetComponent<Animal>();
            animal.WanderingBounds = Bounds;
            instances.Add(animal);
        }

        return instances;
    }

    static Vector3 ChooseRandomTargetPosition(Bounds bounds, int z)
    {
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Bounds.center, Bounds.size);
    }
}
