using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public Equipment.EquipmentTypes EquipmentTypes;
    public Weapon.WeaponTypes WeaponType;
    public override void OnPointerDown(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right && !InventoryManager.Instance.IsPickedItem)
        {
            if (itemKeeper.Item != null)
            {   
                Character.Instance.Putoff(itemKeeper.Item);
                itemKeeper.ReduceAmount();
                InventoryManager.Instance.HideToolTip();
            }
            else
            {
                return;
            }

        }
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; }
        Item pickedItem = InventoryManager.Instance.PickedItemKeeper.Item;
        if (InventoryManager.Instance.IsPickedItem)
        {

            if (IsRightItem(pickedItem))
            {
                if (IsSlotEmpty)
                {
                    InventoryManager.Instance.ReducePickedItem(InventoryManager.Instance.PickedItemKeeper.Amount);
                    StoreItemInKeeper(pickedItem);
                }
                else
                {
                    itemKeeper.ExChange(InventoryManager.Instance.PickedItemKeeper);
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            if (IsSlotEmpty)
            {
                return;
            }
            else
            {
                InventoryManager.Instance.PcikUpItem(itemKeeper.Item, itemKeeper.Amount);
                itemKeeper.ReduceAmount(itemKeeper.Amount);
            }
        }
    }
    public bool IsRightItem(Item item)
    {
        return ((item is Equipment && (item as Equipment).EquipmentType == EquipmentTypes) || (item is Weapon && (item as Weapon).WeaponType == WeaponType)) ? true : false;
    }
}
