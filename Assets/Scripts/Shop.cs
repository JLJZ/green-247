using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    Inventory<Animal> Inventory;

    [SerializeField, Min(1)] int _rollCost = 1;
    public int RollCost => _rollCost;

    void Start()
    {
        Inventory = InventoryService.Instance.Animals;
    }

    public bool HasEnoughTickets => InventoryService.Instance.Tickets >= RollCost;

    public List<Animal> Roll(int tickets)
    {
        if (tickets < RollCost)
        {
            Debug.LogError("Tickets must be greater than 0.");
            return null;
        }

        List<Animal> animals = new(tickets);
        for (int i = 0; i < tickets / RollCost; i++)
        {
            var animal = Roll();

            animals.Add(animal);
            Inventory.Add(animal);
            InventoryService.Instance.SpendTickets(RollCost);
        }
        return animals;
    }

    Animal Roll() => Inventory.AllValidItems
            .OrderBy(_ => Random.Range(0, 1f))
            .First();
}
