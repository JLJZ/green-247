using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class LocationUnlockList : MonoBehaviour
{
    [SerializeField] GameObject LocationEntryPrefab;

    [SerializeField] Color Selected, Normal;
    [SerializeField] Color NameSelected, NameNormal;

    private LocationUnlockPanel SelectedPanel;

    private List<LocationUnlockPanel> panels = new();

    void OnValidate()
    {
        Assert.IsTrue(LocationEntryPrefab.GetComponent<LocationUnlockPanel>());
    }

    void Start()
    {
        var scrollRect = GetComponent<ScrollRect>();
        var locations = InventoryService.Instance.Locations.AllValidItems;
        foreach (var location in locations)
        {
            var entry = Instantiate(LocationEntryPrefab, scrollRect.content)
                .GetComponent<LocationUnlockPanel>();

            if (location == InventoryService.Instance.CurrentLocation)
            {
                entry.SetColor(Selected);
                entry.SetNameColor(NameSelected);
                SelectedPanel = entry;
            }
            else
            {
                entry.SetColor(Normal);
                entry.SetNameColor(NameNormal);
            }

            entry.OnSelect += OnSelect;
            entry.gameObject.SetActive(true);
            entry.Init(location);
            panels.Add(entry);
        }
    }

    void OnDestroy()
    {
        foreach (var panel in panels)
        {
            panel.OnSelect -= OnSelect;
        }
    }

    void OnSelect(object o, EventArgs e)
    {
        LocationUnlockPanel panel = (LocationUnlockPanel)o;
        if (panel == SelectedPanel)
            return;

        if (!panel.AlreadyUnlocked)
            return;

        if (SelectedPanel)
        {
            SelectedPanel.SetColor(Normal);
            SelectedPanel.SetNameColor(NameNormal);
        }

        panel.SetColor(Selected);
        panel.SetNameColor(NameSelected);
        SelectedPanel = panel;
        InventoryService.Instance.SetCurrentLocation(panel.Location);
    }
}
