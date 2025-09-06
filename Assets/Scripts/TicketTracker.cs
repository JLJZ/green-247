using TMPro;
using UnityEngine;

public class TicketTracker : MonoBehaviour
{
    [SerializeField] TMP_Text tickets;

    void Start()
    {
        InventoryService.Instance.TicketsChanged += OnTicketsChanged;
        tickets.text = InventoryService.Instance.Tickets.ToString();
    }

    void OnDestroy()
    {
        if (InventoryService.Instance == null) return;
        InventoryService.Instance.TicketsChanged -= OnTicketsChanged;
    }

    void OnTicketsChanged(object sender, int value)
    {
        tickets.text = value.ToString();
    }
}
