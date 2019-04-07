using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject itemPrefab;
    public ItemKeeper itemKeeper;

    public bool IsSlotEmpty
    {
        get
        {
            if (transform.childCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private void StoreItemNewKeeper(Item item, int amount = 1)
    {
        GameObject itemGameobject = Instantiate(itemPrefab);
        itemGameobject.transform.SetParent(this.transform);
        itemGameobject.transform.localPosition = Vector3.zero;
        itemKeeper = itemGameobject.GetComponent<ItemKeeper>();
        itemGameobject.transform.localScale = Vector3.one;
        (itemGameobject.transform as RectTransform).sizeDelta = (gameObject.transform as RectTransform).sizeDelta;
        itemKeeper.SetItemKeeper(item, amount);
    }
    public void ClearItemKeeper()
    {
        if (itemKeeper != null)
        {
            itemKeeper.ReduceAmount(itemKeeper.Amount);
        }
    }
    public void StoreItemInKeeper(Item item, int amount = 1)
    {
        if (IsSlotEmpty)
        {
            StoreItemNewKeeper(item, amount);
        }
        else if (itemKeeper.IsEmpty)
        {
            itemKeeper.SetItemKeeper(item, amount);
        }
        else
        {
            itemKeeper.AddAmount(amount);
        }

    }
    public int GetItemId()
    {
        if (itemKeeper.IsEmpty)
        { return -1; }

        return itemKeeper.Item.ID;
    }
    public bool IsFiled()
    {
        return itemKeeper.Amount >= itemKeeper.Item.Capacity ? true : false;
    }
    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!IsSlotEmpty &&
        !InventoryManager.Instance.IsPickedItem && !IsSlotEmpty && !itemKeeper.IsEmpty)
        {
            string toolTipText = itemKeeper.Item.GetTooltipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }
    }
    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!IsSlotEmpty)
        {
            InventoryManager.Instance.HideToolTip();
        }
    }
    public virtual void OnPointerDown(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right && !InventoryManager.Instance.IsPickedItem)
        {
            if (!IsSlotEmpty && (itemKeeper.Item is Equipment || itemKeeper.Item is Weapon))
            {
                Item item = itemKeeper.Item;
                itemKeeper.ReduceAmount();
                Character.Instance.PutOn(item);
                InventoryManager.Instance.HideToolTip();
            }

        }
        if (pointerEventData.button != PointerEventData.InputButton.Left) { return; }
        if (!IsSlotEmpty && !itemKeeper.IsEmpty)
        {
            if (!InventoryManager.Instance.IsPickedItem)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    int amountPicked = (itemKeeper.Amount + 1) / 2;
                    InventoryManager.Instance.PcikUpItem(itemKeeper.Item, amountPicked);
                    itemKeeper.ReduceAmount(amountPicked);
                    #region //
                    // int amountRemain = itemKeeper.Amount - amountPicked;
                    // if (amountRemain <= 0)
                    // {
                    //     Destroy(itemKeeper.gameObject);
                    // }
                    // else
                    // {
                    //     itemKeeper.SetAmount(amountRemain);
                    // }
                    #endregion
                }
                else
                {
                    InventoryManager.Instance.PcikUpItem(itemKeeper.Item, itemKeeper.Amount);
                    itemKeeper.ReduceAmount(itemKeeper.Amount);
                }
            }
            else
            {
                if (itemKeeper.Item.ID == InventoryManager.Instance.PickedItemKeeper.Item.ID)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        if (itemKeeper.Amount < itemKeeper.Item.Capacity)
                        {
                            itemKeeper.AddAmount();
                            InventoryManager.Instance.ReducePickedItem();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (itemKeeper.Amount < itemKeeper.Item.Capacity)
                        {
                            int amountRemain = itemKeeper.Item.Capacity - itemKeeper.Amount;
                            if (amountRemain >= InventoryManager.Instance.PickedItemKeeper.Amount)
                            {
                                itemKeeper.AddAmount(InventoryManager.Instance.PickedItemKeeper.Amount);
                                InventoryManager.Instance.ReducePickedItem(InventoryManager.Instance.PickedItemKeeper.Amount);
                            }
                            else
                            {
                                itemKeeper.AddAmount(amountRemain);
                                InventoryManager.Instance.ReducePickedItem(amountRemain);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    itemKeeper.ExChange(InventoryManager.Instance.PickedItemKeeper);
                }
            }
        }
        else
        {
            if (InventoryManager.Instance.IsPickedItem)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    StoreItemInKeeper(InventoryManager.Instance.PickedItemKeeper.Item);
                    InventoryManager.Instance.ReducePickedItem();
                }
                else
                {
                    StoreItemInKeeper(InventoryManager.Instance.PickedItemKeeper.Item, InventoryManager.Instance.PickedItemKeeper.Amount);
                    InventoryManager.Instance.ReducePickedItem(InventoryManager.Instance.PickedItemKeeper.Amount);
                }
            }
            else
            {
                return;
            }
        }
    }


}
