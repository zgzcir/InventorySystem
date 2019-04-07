using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class Consumable : Item
{
    public int Hp { get; set; }
    public int Mp { get; set; }

    public Consumable(int hp, int mp, int id, string name, ItemTypes itemType, QualityTypes quality, string description, int capacity, int buyprice, int sellprice, string spritepath)
    : base(id, name, itemType, quality, description, capacity, buyprice, sellprice, spritepath)
    {
        this.Hp = hp;
        this.Mp = mp;
    }
    public override string GetTooltipText()
    {
        string baseText = base.GetTooltipText();
        string text;
        if (Hp == 0)
        {
            text = string.Format("{0}\n\n<color=blue>加蓝：{1}</color>", baseText, Mp);
        }
        else
        {
            text = string.Format("{0}\n\n<color=blue>加血：{1}</color>", baseText, Hp);
        }
        return text;
    }
}


