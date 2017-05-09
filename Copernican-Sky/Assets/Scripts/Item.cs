
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
//TODO make armour, 

//key(normal) item
public class KeyItem : IItem
{
    protected int itemId;
    protected int questId;
    public KeyItem(string itemName, double weight, int sellPrice, int buyPrice, int itemId, int questId)
    {
        this.weight = weight;
        this.itemName = itemName;
        this.sellPrice = sellPrice;
        this.buyPrice = buyPrice;
        this.itemId = itemId;
        this.questId = questId;
        this.itemClass = ItemClass.K;
        //TODO math to set these values. These are DUMMY VALUES
        skillBonus = new Skills(new int[7] { 0, 0, 0, 0, 0, 0, 0 });
    }
    //getters specific to weapons 
    override public int getIntValue(string value)
    {
        switch (value)
        {
            case "itemId":
                return itemId;
            case "questId":
                return questId;
            default:
                throw new System.Exception();
        }
    }
}

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
    public static readonly IItem COIN = new KeyItem("Coin", 0.0, 1, 1, 0, 0);
    public static readonly IItem AUNT_MAYS_COOKIES = new KeyItem("Aunt May's cookies", 2, 0, 1, 1, 0);
    public static readonly IItem KEY = new KeyItem("Chest Key", 0.1,0,0,0,1);
}
