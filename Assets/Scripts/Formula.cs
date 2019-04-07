using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula
{
    public int Item1ID { get; private set; }
    public int Item1Amount { get; private set; }
    public int Item2ID { get; private set; }
    public int Item2Amount { get; private set; }
    public int ResId { get; private set; }
  
    public List<int> NeedMaterialIds
    {
        get; private set;
    }

    public Formula(int item1ID, int item1Amount, int item2iD, int item2Amount, int resID)
    {
        this.Item1ID = item1ID;
        this.Item1Amount = item1Amount;
        this.Item2ID = item2iD;
        this.Item2Amount = item2Amount;
        this.ResId = resID;
        NeedMaterialIds = new List<int>();
        for (int i = 0; i < Item1Amount; i++)
        {
            NeedMaterialIds.Add(Item1ID);
        }
        for (int i = 0; i < Item2Amount; i++)
        {
            NeedMaterialIds.Add(Item2ID);
        }
      
    }
    public bool Match(List<int> ids)
    {
        List<int> tempIds = new List<int>(ids);
        foreach (int id in NeedMaterialIds)
        {
            if (!tempIds.Remove(id))
            {
                return false;
            }
        }
        return true;
    }
}
