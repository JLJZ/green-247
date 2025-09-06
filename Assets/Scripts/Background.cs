using UnityEngine;

public class Background : MonoBehaviour
{
    Location CurrentLocation;

    private void Start()
    {
        var location = InventoryService.Instance.CurrentLocation;
        CurrentLocation = Instantiate(location, transform);
    }

    private void OnDestroy()
    {
        Destroy(CurrentLocation);
        CurrentLocation = null;
    }
}
