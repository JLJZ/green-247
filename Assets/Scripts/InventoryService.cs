using System;
using UnityEngine;

[Serializable]
public class InventoryService : Service<InventoryService>
{
    [SerializeField]
    AnimalInventory _animals = new();

    [SerializeField]
    bool addAnimals = false;

    public Inventory<Animal> Animals => _animals;

    protected override void Awake()
    {
        base.Awake();
        if (addAnimals)
        {
            foreach (var animal in Animals.AllValidItems)
            {
                Debug.Log($"Adding {animal.name}");
                Animals.Add(animal);
            }
        }
    }
}

[Serializable]
public class AnimalInventory : Inventory<Animal>
{
    public AnimalInventory() : base("Animals") { }
}
