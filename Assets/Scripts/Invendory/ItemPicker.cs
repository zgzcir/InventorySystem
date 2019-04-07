using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : ItemKeeper
{
    protected override void UpdateUI()
    {
        if (Amount == 0)
        {
            Hide();
            return;
        }
        Show();
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
        {
            AmountText.text = "";
        }
    }



}
