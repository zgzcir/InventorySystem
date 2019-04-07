using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class Inventory : MonoBehaviour
{


    protected Slot[] slots;
    protected virtual void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
    }
    protected virtual void Start()
    {
        transform.Find("cBtn").GetComponent<Button>().onClick.AddListener(() => { gameObject.SetActive(false); });

    }
    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }





    public bool StoreItem(Item item, int amount = 1, Slot chosseSlot = null)
    {
        if (item == null)
        {
            Debug.LogWarning("Item Id Not Exits");
            return false;
        }

        if (item.Capacity == 1)
        {
            Slot slot = chosseSlot;
            if (slot == null)
            {
                slot = FindEmptySlot();
            }
            if (slot == null)
            {
                Debug.LogWarning("No Empty Slot Exits");
            }
            else
            {
                slot.StoreItemInKeeper(item, amount);
            }
        }
        else
        {
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItemInKeeper(item, amount);
            }
            else
            {
                Slot emptySlot;
                if (chosseSlot == null)
                {
                    emptySlot = FindEmptySlot();
                }

                else
                {
                    emptySlot = chosseSlot;
                }
                if (emptySlot != null)
                {
                    emptySlot.StoreItemInKeeper(item, amount);
                }
                else
                {
                    Debug.LogWarning("No Empty Slot Exits");
                    return false;
                }
            }
        }
        return true;
    }








    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            if (slot.IsSlotEmpty || slot.itemKeeper.IsEmpty)
            {
                return slot;
            }
        }
        return null;
    }
    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (!slot.IsSlotEmpty && !slot.itemKeeper.IsEmpty)
            {
                if (item.ID == slot.GetItemId() && !slot.IsFiled())
                {
                    return slot;
                }
            }
        }
        return null;
    }

    #region  SaveAndLoad
    public void SaveInvetory()
    {

        StringBuilder sb = new StringBuilder();
        foreach (Slot slot in slots)
        {
            if (!slot.IsSlotEmpty && !slot.itemKeeper.IsEmpty)
            {
                sb.Append(slot.itemKeeper.Item.ID);
                sb.Append(',');
                sb.Append(slot.itemKeeper.Amount);
                sb.Append('+');
            }
            else
            {
                sb.Append("~+");
            }
        }
        sb.Remove(sb.Length - 1, 1);
     
        PlayerPrefs.SetString(this.gameObject.name, sb.ToString());
    }

    public void LoadInventory()
    {
       
        if (!PlayerPrefs.HasKey(this.gameObject.name))
        {
            return;
        }
        else
        {
            foreach (Slot slot in slots)
            {
                slot.ClearItemKeeper();
            }
            string str = PlayerPrefs.GetString(this.gameObject.name);
            string[] strs = str.Split('+');
            for(int i=0;i<strs.Length;i++)
            {
                if(strs[i]=='~'.ToString())
                {

                    break;
                }
                else
                {
                    string[] tempStrs = strs[i].Split(',');
                    int id = int.Parse(tempStrs[0]);
                    int amount = int.Parse(tempStrs[1]);
                    slots[i].StoreItemInKeeper(InventoryManager.Instance.GetItemById(id), amount);
                }
            }
        }
    }

    #endregion
}
