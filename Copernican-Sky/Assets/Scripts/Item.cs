
public abstract class IItem
{
    protected double weight;
    protected string itemName;
    protected int sellPrice;
    protected int buyPrice;
    protected ItemClass itemClass;
    protected Skills skillBonus;
    //get methods
    public double Weight { get { return weight; } }
    public string ItemName { get { return itemName; } }
    public ItemClass ItemClass { get { return itemClass; } }
    public Skills SkillBonus { get { return skillBonus; } }
    public int SellPrice { get { return sellPrice; } }
    public int BuyPrice { get { return buyPrice; } }

    public abstract  int getIntValue(string value);
}

public class Weapon : IItem{
    protected WeaponType weaponType;
    protected int baseDamage;
    protected int scalingDamage;
    //combat
    //sword, axe, spear,
    public Weapon(string itemName, double weight, int sellPrice, int buyPrice, ItemClass itemClass, WeaponType weaponType)
    {
        this.weight = weight;
        this.itemName = itemName;
        this.sellPrice = sellPrice;
        this.buyPrice = buyPrice;
        this.itemClass = itemClass;
        this.weaponType = weaponType;
        //TODO math to set these values. These are DUMMY VALUES
        baseDamage = 100;
        scalingDamage = 100;
        skillBonus = new Skills(new int[7] { 1, 1, 1, 1, 1, 1, 1 });

    }
    //getters specific to weapons 
    override public int getIntValue(string value)
    {
        switch (value)
        {
            case "weaponType":
                return (int)weaponType;
            case "baseDamage":
                return baseDamage;
            case "scalingDamage":
                return scalingDamage;
            default:
                throw new System.Exception();
        } 
    }
}
//TODO make armour, quest(normal) item

public enum ItemClass
{
    F,E,D,C,B,A,S,K //k for key item
}

public enum WeaponType
{
    SWORD,AXE,SPEAR
}

public class ItemType
{
    public static readonly IItem SWORD_BASIC = new Weapon("Basic Sword",3,10,20,ItemClass.F, WeaponType.SWORD);
}
