using System;
using UnityEngine;

[Serializable]
public class InventoryService : Service<InventoryService>
{
    [SerializeField]
    AnimalInventory _animals = new();

    [SerializeField]
    bool addAnimals = false;

    [field: SerializeField, Min(0)]
    public int Tickets { get; private set; } = 1;

    public Inventory<Animal> Animals => _animals;

    public EventHandler<int> TicketsChanged { get; set; }

    public int SpendTickets(int amount)
    {
        if (Tickets >= amount)
        {
            Tickets -= amount;
            TicketsChanged?.Invoke(this, Tickets);
            return Tickets;
        }
        return Tickets - amount;
    }

    public int AddTickets(int amount)
    {
        Tickets += amount;
        TicketsChanged?.Invoke(this, Tickets);
        return Tickets;
    }

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
