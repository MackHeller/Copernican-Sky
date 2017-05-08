

public class Item {

    private double weight;
    private string itemName;

    public double Weight{get{return weight;}}
    public string ItemName{get{return itemName;}}

    //combat
    //sword, axe, spear, 
    int weaponGroup;
    int speed; 
    public Item(double weight)
    {
        this.weight = weight;

    }

}

public class ItemType
{
    public static readonly Item SWORD_BASIC = new Item(3.0);
}
