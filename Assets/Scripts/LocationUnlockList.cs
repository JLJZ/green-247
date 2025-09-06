using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class LocationUnlockList : MonoBehaviour
{
    [SerializeField] GameObject LocationEntryPrefab;

    void OnValidate()
    {
        Assert.IsTrue(LocationEntryPrefab.GetComponent<LocationEntry>());
    }

    void Start()
    {
        var scrollRect = GetComponent<ScrollRect>();
        var locations = InventoryService.Instance.Locations.AllValidItems;
        foreach (var location in locations)
        {
            var entry = Instantiate(LocationEntryPrefab, scrollRect.content)
                .GetComponent<LocationEntry>();
            entry.gameObject.SetActive(true);
            entry.Init(location);
        }
    }
}
