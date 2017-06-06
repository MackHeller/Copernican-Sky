//item enums (hover over to see real value, if using MicroSoft visual studio)
public enum ItemClass
{
    F, E, D, C, B, A, S, K //k for key item
}

public enum EquipmentType
{
    SWORD, AXE, SPEAR,
    HELMET, CHEST, GLOVES, BOOTS, PANTS
}

public enum ItemType
{
    SWORD_BASIC,
    COIN,
    AUNT_MAYS_COOKIES,
    KEY
}

//abstract class the represents all types of items (currently Equipment and KeyItem)
public abstract class IItem
{
    protected double weight;
    protected ItemType itemType;
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
    public ItemType ItemType { get { return itemType; } }

    //factory method TODO may need to OVERLOAD for additional params
    public static IItem buildItem(ItemType item)
    {
        switch (item) {
            case ItemType.SWORD_BASIC:
                return new Equipment(item,"Basic Sword", 3, 10, 20, ItemClass.F, EquipmentType.SWORD);
            case ItemType.COIN:
                return new KeyItem(item,"Coin", 0.0, 1, 1, 0, 0);
            case ItemType.AUNT_MAYS_COOKIES:
                return new KeyItem(item,"Aunt May's Cookies", 2, 0, 1, 1, 0);
            case ItemType.KEY:
                return new KeyItem(item,"Chest Key", 0.1, 0, 0, 0, 1);
            default:
                throw new System.Exception();
        }
    }

    //abstract methods
    public abstract  int getIntValue(string value);

    private class Equipment : IItem
    {
        protected EquipmentType equipmentType;
        protected int baseValue;
        protected int scalingValue;
        //combat
        //sword, axe, spear,
        public Equipment(ItemType itemType, string itemName, double weight, int sellPrice, int buyPrice, ItemClass itemClass, EquipmentType equipmentType)
        {
            this.weight = weight;
            this.itemName = itemName;
            this.sellPrice = sellPrice;
            this.buyPrice = buyPrice;
            this.itemClass = itemClass;
            this.equipmentType = equipmentType;
            //TODO math to set these values. These are DUMMY VALUES
            baseValue = 100;
            scalingValue = 100;
            skillBonus = new Skills(new int[8] { 1, 1, 1, 1, 1, 1, 1, 1 });

        }
        //getters specific to weapons 
        override public int getIntValue(string value)
        {
            switch (value)
            {
                case "equipmentType":
                    return (int)equipmentType;
                case "baseValue":
                    return baseValue;
                case "scalingValue":
                    return scalingValue;
                default:
                    throw new System.Exception();
            }
        }
    }

    //key(normal) item
    protected class KeyItem : IItem
    {
        protected int itemId; //tag for item
        protected int questId; //item's quest 
        public KeyItem(ItemType itemType, string itemName, double weight, int sellPrice, int buyPrice, int itemId, int questId)
        {
            this.weight = weight;
            this.itemName = itemName;
            this.sellPrice = sellPrice;
            this.buyPrice = buyPrice;
            this.itemId = itemId;
            this.questId = questId;
            this.itemClass = ItemClass.K;
            //TODO math to set these values. These are DUMMY VALUES
            skillBonus = new Skills(new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 });
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
}


