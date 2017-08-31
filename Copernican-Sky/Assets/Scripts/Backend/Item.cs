//item enums (hover over to see real value, if using MicroSoft visual studio)
//class is how good an item is 
public enum ItemClass
{
    F, E, D, C, B, A, S, K //k for key item
}
//type is what kind of an item an item is
public enum EquipmentType
{
    //weapons
    SWORD, AXE, SPEAR,
    //clothes 
    HELMET, CHEST, GLOVES, BOOTS, PANTS
}

//abstract class the represents all types of items (currently Equipment and KeyItem)
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

    /*
     * static factory method for items; Builds an item for you based off a given enum. 
     * NOTE: you cannot create items directly you MUST use this method. For more on factory methods see:
     * https://en.wikipedia.org/wiki/Factory_method_pattern
     * TODO may need to OVERLOAD for additional params
     * @param   item    item enum to build
     * @return          an IItem object
     * */
    public static IItem buildItem(string name)
    {
        switch (name) {
            case "Wooden Sword":
                return new Equipment(name, 3, 10, 20, ItemClass.F, EquipmentType.SWORD);
            case "Coin":
                return new KeyItem(name, 0.0, 1, 1, 0, 0);
            case "Aunt May's Cookies":
                return new KeyItem(name, 2, 0, 1, 1, 0);
            case "Key":
                return new KeyItem(name, 0.1, 0, 0, 0, 1);
            default:
                throw new System.Exception("Item not found");
        }
    }

    //abstract methods
    public abstract  int getIntValue(string value);

    /**
     * object for equipable objects such as clothes and weapons
     * */
    private class Equipment : IItem
    {
        protected EquipmentType equipmentType;
        protected int baseValue;
        protected int scalingValue;
        //combat
        //sword, axe, spear,
        public Equipment(string itemName, double weight, int sellPrice, int buyPrice, ItemClass itemClass, EquipmentType equipmentType)
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
        /*  
         *  getters specific to equiment
         *  @param  value   the name of the item you want 
         *                  currently "equipmentType" or "baseValue" or "scalingValue"
         *  @return         the value you requested                  
         * */
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
        public override string ToString()
        {
            string words = this.ItemName+" "+this.ItemClass+" "+this.Weight;
            return words;
        }
    }

    /**
     * object for key objects such as quest items, keys, gold, gems, etc. 
     * */
    protected class KeyItem : IItem
    {
        protected int itemId; //tag for item
        protected int questId; //item's associated quest (could be 0 for no quest)
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
            skillBonus = new Skills(new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 });
        }
        /*  
         *  getters specific to key items 
         *  @param  value   the name of the item you want 
         *                  currently "itemId" or "questId"
         *  @return         the value you requested
         * */
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
        public override string ToString()
        {
            string words = this.ItemName + " " + this.ItemClass + " " + this.Weight;
            return words;
        }
    }
}


