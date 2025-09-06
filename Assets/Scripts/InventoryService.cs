using System;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class InventoryService : Service<InventoryService>
{
    [SerializeField]
    AnimalInventory _animals = new();

    [SerializeField]
    bool addAnimals = false;

    [SerializeField]
    LocationInventory _locations = new();

    [SerializeField]
    Location DefaultLocation;

    [field: SerializeField, Min(0)]
    public int Tickets { get; private set; } = 1;

    public Inventory<Animal> Animals => _animals;

    public Inventory<Location> Locations => _locations;

    public Location CurrentLocation { get; private set; }

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

    public bool SetCurrentLocation(Location location)
    {
        if (!Locations.Contains(location))
            return false;
        CurrentLocation = location;
        return true;
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
        Assert.IsNotNull(DefaultLocation);
        Locations.Add(DefaultLocation);
        CurrentLocation = DefaultLocation;
    }
}

[Serializable]
public class AnimalInventory : Inventory<Animal>
{
    public AnimalInventory() : base("Animals") { }
}

[Serializable]
public class LocationInventory : Inventory<Location>
{
    public LocationInventory() : base("Locations") { }
}
