using TMPro;
using UnityEngine;

public class TicketTracker : MonoBehaviour
{
    [SerializeField] TMP_Text tickets;

    void Start()
    {
        InventoryService.Instance.TicketsChanged += OnTicketsChanged;
    }

    void OnDestroy()
    {
        InventoryService.Instance.TicketsChanged -= OnTicketsChanged;
    }

    void OnTicketsChanged(object sender, int value)
    {
        tickets.text = value.ToString("D3");
    }
}
