
public class Equipment : Item
{

    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public EquipmentTypes EquipmentType { get; set; }

    public Equipment(int strength, int intellect, int agility, int stamina, EquipmentTypes equipmentType, int id, string name, ItemTypes itemType, QualityTypes quality, string description, int capacity, int buyprice, int sellprice, string spritepath)
     : base(id, name, itemType, quality, description, capacity, buyprice, sellprice, spritepath)
    {
        this.Strength = strength;
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.EquipmentType = equipmentType;
    }

    public enum EquipmentTypes
    {None,
        Head,
        Neck,
        Chest,
        Ring,
        Leg,
        Boots,
        Bracer,
        Trinket,
        Shoulder,
        Belt,
        OffHand
    }

    public override string GetTooltipText()
    {
        string text = base.GetTooltipText();

        string equipTypeText = "";
        switch (EquipmentType)
        {
            case EquipmentTypes.Head:
                equipTypeText = "头部";
                break;
            case EquipmentTypes.Neck:
                equipTypeText = "脖子";
                break;
            case EquipmentTypes.Chest:
                equipTypeText = "胸部";
                break;
            case EquipmentTypes.Ring:
                equipTypeText = "戒指";
                break;
            case EquipmentTypes.Leg:
                equipTypeText = "腿部";
                break;
            case EquipmentTypes.Bracer:
                equipTypeText = "护腕";
                break;
            case EquipmentTypes.Boots:
                equipTypeText = "靴子";
                break;
            case EquipmentTypes.Shoulder:
                equipTypeText = "护肩";
                break;
            case EquipmentTypes.Belt:
                equipTypeText = "腰带";
                break;
            case EquipmentTypes.OffHand:
                equipTypeText = "副手";
                break;
        }
        string newText = string.Format("{0}\n\n<color=blue>装备类型：{1}\n力量：{2}\n智力：{3}\n敏捷：{4}\n体力：{5}</color>", text, equipTypeText, Strength, Intellect, Agility, Stamina);
        return newText;
    }

}