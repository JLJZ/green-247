using System;
using UnityEngine;

public class InventoryService : MonoBehaviour
{
    [SerializeField]
    AnimalInventory Animals = new AnimalInventory();

    [SerializeField]
    bool addAnimals = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (addAnimals)
        {
            foreach (var animal in Animals.All.Value.Values)
            {
                Debug.Log($"Adding {animal.name}");
                Animals.Add(animal);
            }
        }
    }
}

[Serializable]
class AnimalInventory : Inventory<Animal>
{
    public AnimalInventory() : base("Animals") { }
}
