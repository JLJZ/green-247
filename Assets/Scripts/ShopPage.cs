using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ShopPage : MonoBehaviour
{
    [SerializeField] Shop Shop;

    [SerializeField] Canvas ShopCanvas;
    [SerializeField] Canvas RollCanvas;
    [SerializeField] Button RollButton;
    [SerializeField] ShopResultPage ShopResultPage;

    protected virtual void Awake()
    {
        Assert.IsNotNull(Shop);
        Assert.IsNotNull(ShopCanvas);
        Assert.IsNotNull(RollCanvas);
        Assert.IsNotNull(RollButton);
        Assert.IsNotNull(ShopResultPage);
    }

    void Start()
    {
        ShopCanvas.enabled = true;
        RollCanvas.enabled = false;
        RollButton.interactable = Shop.HasEnoughTickets;
        ShopResultPage.Hide();
    }

    void OnEnable()
    {
        RollButton.onClick.AddListener(Roll);
    }

    void OnDisable()
    {
        RollButton.onClick.RemoveAllListeners();
    }

    void Roll()
    {
        List<Animal> animals = Shop.Roll(Shop.RollCost);
        if (animals != null)
        {
            ShopCanvas.enabled = false;
            RollCanvas.enabled = true;
        }
        RollButton.interactable = Shop.HasEnoughTickets;
    }
}
