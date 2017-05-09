

public class Item {

    private double weight;
    private string itemName;

    public double Weight{get{return weight;}}
    public string ItemName{get{return itemName;}}

    //combat
    //sword, axe, spear, 
    int weaponGroup;
    int speed; 
    public Item(string itemName, double weight)
    {
        this.weight = weight;
        this.itemName = itemName;
    }

}

public class ItemType
{
    public static readonly Item SWORD_BASIC = new Item("Basic Sword",3.0);
}
