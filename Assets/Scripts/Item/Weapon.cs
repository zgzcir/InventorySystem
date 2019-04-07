public class Weapon : Item
{

    public int Damage { get; set; }
    public WeaponTypes WeaponType { get; set; }

    public Weapon(int damage, WeaponTypes weaponType, int id, string name, ItemTypes itemType, QualityTypes quality, string description, int capacity, int buyprice, int sellprice, string spritepath)
         : base(id, name, itemType, quality, description, capacity, buyprice, sellprice, spritepath)
    {
        this.Damage = damage;
        this.WeaponType = weaponType;
    }
    public enum WeaponTypes
    {
        None,
        MainHand,
        OffHand
    }



    public override string GetTooltipText()
    {
        string text = base.GetTooltipText();

        string wpTypeText = "";

        switch (WeaponType)
        {
            case WeaponTypes.OffHand:
                wpTypeText = "副手";
                break;
            case WeaponTypes.MainHand:
                wpTypeText = "主手";
                break;
        }

        string newText = string.Format("{0}\n\n<color=blue>武器类型：{1}\n攻击力：{2}</color>", text, wpTypeText, Damage);

        return newText;
    }
}