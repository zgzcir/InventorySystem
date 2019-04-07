using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class Item
{

    public int ID { get; set; }
    public string Name { get; set; }
    public ItemTypes ItemType { get; set; }
    public QualityTypes Quality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string SpritePath { get; set; }

    public Item()
    {   
        this.ID = -1;
    }

    public Item(int id, string name, ItemTypes itemType, QualityTypes quality, string description, int capacity, int buyprice, int sellprice, string spritepath)
    {
        this.ID = id;
        this.Name = name;
        this.ItemType = itemType;
        this.Quality = quality;
        this.Description = description;
        this.Capacity = capacity;
        this.BuyPrice = buyprice;
        this.SellPrice = sellprice;
        this.SpritePath = spritepath;
    }
    public enum ItemTypes
    {
        Consumable,
        Equipment,
        Weapon,
        Material
    }
    public enum QualityTypes
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artifact
    }

    public virtual string GetTooltipText()
    {
         string color = "";
        switch (Quality)
        {
            case QualityTypes.Common:
                color = "white";
                break;
            case QualityTypes.Uncommon:
                color = "lime";
                break;
            case QualityTypes.Rare:
                color = "navy";
                break;
            case QualityTypes.Epic:
                color = "magenta";
                break;
            case QualityTypes.Legendary:
                color = "orange";
                break;
            case QualityTypes.Artifact:
                color = "red";
                break;
        }
        string text = string.Format("<color={4}>{0}</color>\n<size=30><color=green>购买价格：{1} 出售价格：{2}</color></size>\n<color=yellow><size=20>{3}</size></color>", Name, BuyPrice, SellPrice, Description, color);
        return text;
    }

}


