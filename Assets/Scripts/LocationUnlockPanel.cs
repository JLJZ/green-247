using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class LocationUnlockPanel : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] GameObject ImageContainer;
    [SerializeField] TMP_Text LocationName;
    [SerializeField] Button UnlockButton;
    [SerializeField] Button SelectButton;

    [Header("Optional")]
    [SerializeField] Location _location;

    public Location Location => _location;

    public EventHandler OnSelect { get; set; } = delegate { };

    public bool AlreadyUnlocked => InventoryService.Instance.Locations.Contains(Location);

    public void SetColor(Color color)
    {
        if (TryGetComponent<Image>(out var image))
            image.color = color;
    }

    public void SetNameColor(Color color) => LocationName.color = color;

    public void Init(Location location)
    {
        if (location == null)
        {
            Debug.LogError("Location is null");
            gameObject.SetActive(false);
            return;
        }

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (location == Location)
            return;

        _location = location;
        LocationName.text = Location.DisplayName;
        InitButton();
        InitImages();
        SelectButton.onClick.AddListener(() => OnSelect(this, EventArgs.Empty));
    }

    void InitButton()
    {
        UnlockButton.gameObject.SetActive(!AlreadyUnlocked);
        if (AlreadyUnlocked)
        {
            UnlockButton.interactable = false;
        }
        else
        {
            UnlockButton.interactable = CanUnlock();
            UnlockButton.onClick.AddListener(UnlockLocation);
        }
    }

    void InitImages()
    {
        var animals = Location.Animals;
        foreach (var animal in animals)
        {
            var image = new GameObject(
                "[Image] " + animal.name,
                typeof(Image)
            ).GetComponent<Image>();

            image.sprite = animal.Sprite;
            image.transform.SetParent(ImageContainer.transform);
            if (InventoryService.Instance.Animals.Contains(animal))
            {
                image.color = Color.white;
            }
            else
            {
                image.color = Color.gray;
            }
        }
    }

    void Awake()
    {
        Assert.IsNotNull(ImageContainer);
        Assert.IsNotNull(LocationName);
        Assert.IsNotNull(UnlockButton);
    }

    void Start()
    {
        if (Location)
            Init(Location);
        else
            gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        UnlockButton.onClick.RemoveAllListeners();
        SelectButton.onClick.RemoveAllListeners();
    }

    bool CanUnlock()
    {
        var foundAnimals = InventoryService.Instance.Animals;
        return Location.Animals.All(foundAnimals.Contains);
    }

    void UnlockLocation() => InventoryService.Instance.Locations.Add(Location);
}
