using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : Inventory
{
    public int[] ItemIdArray;
    private Player player;

    private static Vendor _instance;
    public static Vendor Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Vendor").GetComponent<Vendor>();
            }
            return _instance;
        }
    }
    protected override void Start()
    {
        base.Start();
        InitShop();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void InitShop()
    {
        foreach (int id in ItemIdArray)
        {
            StoreItem(id);
        }
    }
    public void BuyItem(Item item)
    {
        bool isBuySuccess = player.Cosume(item.BuyPrice);
        if (isBuySuccess)
        {
            Knapsack.Instance.StoreItem(item);
        }
    }
    public void SellItem(int amount)
    {
        int coinAmount = InventoryManager.Instance.PickedItemKeeper.Item.SellPrice * amount;
        player.Earn(coinAmount);
        InventoryManager.Instance.PickedItemKeeper.ReduceAmount(amount);
    }
}
