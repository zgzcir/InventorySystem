using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forge : Inventory
{

    private static Forge _instance;
    public static Forge Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Forge").GetComponent<Forge>();
            }
            return _instance;
        }
    }

    private List<Formula> formulas;
    private Button forgeButton;
    protected override void Awake()
    {
        base.Awake();
        ParseFormulaJson();
    }
    protected override void Start()
    {
        transform.Find("ForgeButton").GetComponent<Button>().onClick.AddListener(ForgeItem);
    }

    private void ParseFormulaJson()
    {
        formulas = new List<Formula>();
        TextAsset formulaText = Resources.Load<TextAsset>("Formulas");
        string formulasJson = formulaText.text;
        JSONObject jo = new JSONObject(formulasJson);
        foreach (var temp in jo.list)
        {
            int item1ID = (int)temp["Item1ID"].n;
            int item1Amount = (int)temp["Item1Amount"].n;
            int item2ID = (int)temp["Item2ID"].n;
            int item2Amount = (int)temp["Item2Amount"].n;
            int resID = (int)temp["ResId"].n;
            formulas.Add(new Formula(item1ID, item1Amount, item2ID, item2Amount, resID));
        }
    }
    private void ForgeItem()
    {
        List<int> haveMaterialIDs = new List<int>();
        foreach (Slot slot in slots)
        {
            if (!slot.IsSlotEmpty && !slot.itemKeeper.IsEmpty)
            {
                for (int i = 0; i < slot.itemKeeper.Amount; i++)
                {
                    haveMaterialIDs.Add(slot.itemKeeper.Item.ID);
                }
            }
        }
        Formula temp;
        foreach (Formula f in formulas)
        {
            if (f.Match(haveMaterialIDs))
            { 
                foreach (int id in f.NeedMaterialIds)
                {  
                    foreach (Slot slot in slots)
                    {     
                        if (!slot.itemKeeper.IsEmpty&&slot.itemKeeper.Item.ID == id)
                        {
                            slot.itemKeeper.ReduceAmount();
                            break;
                        }
                    }
                }
                Knapsack.Instance.StoreItem(f.ResId);
                break;
            }
        }

    }
}
