using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Character : Inventory
{
    public Player player;
    public Text propertyText;
    protected override void Start()
    {
        base.Start();
        UpdatePropertyText();
    }
    public void UpdatePropertyText()
    {
        int strength = 0;
        int intellect = 0;
        int agility = 0;
        int stamina = 0;
        int damage = 0;
        strength += player.BasicStrength;
        intellect += player.BasicIntellect;
        agility += player.BasicAgility;
        stamina += player.BasicStamina;
        damage += player.BasicDamage;
        foreach (EquipmentSlot es in slots)
        {
            if (!es.IsSlotEmpty)
            {
                Item item = es.itemKeeper.Item;
                if (item is Equipment)
                {
                    Equipment e = (Equipment)item;
                    strength += e.Strength;
                    intellect += e.Intellect;
                    agility += e.Agility;
                    stamina += e.Stamina;
                }
                else if (item is Weapon)
                {
                    Weapon w = (Weapon)item;
                    damage += w.Damage;
                }

            }
        }
        string s = string.Format("<color=orange>力量：{0}</color>\n<color=#E6E6FA>智力：{1}</color>\n敏捷：{2}\n体力：{3}\n伤害：{4}\n", strength, intellect, agility, stamina, damage);
        propertyText.text = s;
    }
    private static Character _instance;
    public static Character Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Character").GetComponent<Character>();
            }
            return _instance;
        }
    }
    public void PutOn(Item item)
    {
        foreach (EquipmentSlot eSlot in slots)
        {
            if (eSlot.IsRightItem(item))
            {
                if (eSlot.IsSlotEmpty || eSlot.itemKeeper.IsEmpty)
                {
                    eSlot.StoreItemInKeeper(item);
                }
                else if (!eSlot.itemKeeper.IsEmpty)
                {
                    Item tempItem = eSlot.itemKeeper.Item;
                    eSlot.itemKeeper.SetItemKeeper(item, 1);
                    Knapsack.Instance.StoreItem(tempItem);
                }
                break;
            }
        }
    }
    public void Putoff(Item item)
    {
        Knapsack.Instance.StoreItem(item);
    }
}
