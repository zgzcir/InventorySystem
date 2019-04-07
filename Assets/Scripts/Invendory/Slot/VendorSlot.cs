using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class VendorSlot : Slot
{
    public override void OnPointerDown(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right && !InventoryManager.Instance.IsPickedItem)
        {
            if (!IsSlotEmpty && !itemKeeper.IsEmpty)
            {
                Item tempItem = itemKeeper.Item;
                transform.parent.parent.SendMessage("BuyItem", tempItem);
                itemKeeper.ReduceAmount();
            }
        }
        else if (pointerEventData.button == PointerEventData.InputButton.Left && InventoryManager.Instance.IsPickedItem)
        {
            int sellAmount = InventoryManager.Instance.PickedItemKeeper.Amount;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                sellAmount = 1;
            }
            transform.parent.parent.SendMessage("SellItem", sellAmount);

        }

    }
}
