using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class ShopResultPage : ShopPage
{
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text ItemName;
    [SerializeField] TMP_Text ItemRarity;
    [SerializeField] TMP_Text ItemDescription;
    [SerializeField] Button BackToHome;

    Canvas Canvas;

    protected override void Awake()
    {
        base.Awake();
        Assert.IsNotNull(ItemName);
        Assert.IsNotNull(ItemImage);
        Assert.IsNotNull(ItemRarity);
        Assert.IsNotNull(ItemDescription);
        Assert.IsNotNull(BackToHome);
        Canvas = GetComponent<Canvas>();
    }

    public void Display(Animal animal)
    {
        ItemName.text = animal.name;
        ItemImage.sprite = animal.Sprite;
        ItemDescription.text = animal.Description;
        ItemRarity.text = animal.Rarity.ToString();
        switch (animal.Rarity)
        {
            case Rarity.Common:
                ItemRarity.color = Color.gray;
                break;
            case Rarity.Rare:
                ItemRarity.color = Color.skyBlue;
                break;
            case Rarity.Special:
                ItemRarity.color = Color.purple;
                break;
        }
    }

    public void Hide()
    {
        Canvas.enabled = false;
    }

    public void Show()
    {
        Canvas.enabled = true;
    }
}
