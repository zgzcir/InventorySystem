using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private ToolTip toolTip;

    private static InventoryManager _instance;
    private bool isTooltipShow = false;
    private Canvas canvas;


    public void SaveInventory()
    {
        Knapsack.Instance.SaveInvetory();
        Chest.Instance.SaveInvetory();
        Character.Instance.SaveInvetory();
        Forge.Instance.SaveInvetory();
    }
    public void LoadInventory()
    {
        Knapsack.Instance.LoadInventory();
        Chest.Instance.LoadInventory();
        Character.Instance.LoadInventory();
        Forge.Instance.LoadInventory();
    }
    public ItemKeeper PickedItemKeeper
    {
        get; set;
    }

    public bool IsPickedItem
    {
        get
        {
            return PickedItemKeeper.Item == null ? false : true;
        }
    }
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        ParseItemJson();
    }
    private void Start()
    {
        toolTip = GameObject.FindObjectOfType<ToolTip>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        PickedItemKeeper = GameObject.Find("pickedItemKeeper").GetComponent<ItemKeeper>();
        PickedItemKeeper.gameObject.SetActive(false);
    }
    private List<Item> items;
    public Item GetItemById(int id)
    {
        foreach (Item item in items)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }
    private void ParseItemJson()
    {
        items = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsJson = itemText.text;
        JSONObject jo = new JSONObject(itemsJson);
        Debug.Log(jo.list.Count);
        foreach (var temp in jo.list)
        {
            string typeStr = temp["itemType"].str;
            Item.ItemTypes type = (Item.ItemTypes)System.Enum.Parse(typeof(Item.ItemTypes), typeStr);
            int id = (int)temp["id"].n;
            string name = temp["name"].str;
            Item.QualityTypes qualityType = (Item.QualityTypes)System.Enum.Parse(typeof(Item.QualityTypes), temp["quality"].str);
            string description = temp["description"].str;
            int capacity = (int)temp["capacity"].n;
            int buyPrice = (int)temp["buyPrice"].n;
            int sellPrice = (int)temp["sellPrice"].n;
            string spritePath = temp["spritePath"].str;
            Item item = null;
            switch (type)
            {
                case Item.ItemTypes.Consumable:
                    {
                        int hp = (int)temp["hp"].n;
                        int mp = (int)temp["mp"].n;
                        item = new Consumable(hp, mp, id, name, type, qualityType, description, capacity, buyPrice, sellPrice, spritePath);
                        break;
                    }
                case Item.ItemTypes.Weapon:
                    {
                        int damage = (int)temp["damage"].n;
                        Weapon.WeaponTypes weaponType = (Weapon.WeaponTypes)System.Enum.Parse(typeof(Weapon.WeaponTypes), temp["weaponType"].str);
                        item = new Weapon(damage, weaponType, id, name, type, qualityType, description, capacity, buyPrice, sellPrice, spritePath);
                        break;
                    }
                case Item.ItemTypes.Equipment:
                    {
                        int strength = (int)temp["strength"].n;
                        int intellect = (int)temp["intellect"].n;
                        int agility = (int)temp["agility"].n;
                        int stamina = (int)temp["stamina"].n;
                        Equipment.EquipmentTypes equipmentType = (Equipment.EquipmentTypes)System.Enum.Parse(typeof(Equipment.EquipmentTypes), temp["equipmentType"].str);
                        item = new Equipment(strength, intellect, agility, stamina, equipmentType, id, name, type, qualityType, description, capacity, buyPrice, sellPrice, spritePath);
                        break;
                    }
                case Item.ItemTypes.Material:
                    {
                        item = new Material(id, name, type, qualityType, description, capacity, buyPrice, sellPrice, spritePath);
                        break;
                    }
            }
            items.Add(item);
        }
    }
    public void ShowToolTip(string content)
    {
        toolTip.Show(content);
        isTooltipShow = true;
    }
    public void HideToolTip()
    {
        toolTip.Hide();
        isTooltipShow = false;
    }
    private Vector2 toolTipPosionOffset = new Vector2(0, 0);
    private void Update()
    {
        if (IsPickedItem && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1))
        {
            PickedItemKeeper.ReduceAmount(PickedItemKeeper.Amount);
        }
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
        if (isTooltipShow)
        {
            toolTip.SetLocalPotion(position + toolTipPosionOffset);
        }
        if (IsPickedItem)
        {

            PickedItemKeeper.SetLocalPotion(position + toolTipPosionOffset);
        }
    }

    public void PcikUpItem(Item item, int amount = 1)
    {
        PickedItemKeeper.SetItemKeeper(item, amount);
        HideToolTip();
    }
    public void ReducePickedItem(int amount = 1)
    {
        PickedItemKeeper.ReduceAmount(amount);
    }
}
