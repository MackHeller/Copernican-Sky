using C5;
/**
 * inventory class. Holds items that the player collects overtime
 * items are stored in a HashDictionary where the key is the item 
 * and the value is the amount of that item that the player has
 * foreach(Item key in inventory.Keys)
 * */
public class Inventory {
    //the item and its amount
    private HashDictionary<IItem, int> inventory;
    private int carryCapacity;
    private double currentWeight;

    public Inventory(int carryCapacity)
    {
        inventory = new HashDictionary<IItem, int>();
        this.carryCapacity = carryCapacity;
        currentWeight = 0.0;
    }

    public double CurrentWeight
    {
        get { return currentWeight; }
    }

    public HashDictionary<IItem, int> InventoryList
    {
        get{return inventory;}
    }

    /*
* Adds an item to the inventory
* */
    public bool addItem(IItem itemToAdd)
    {
        return addItem(itemToAdd, 1);
    }
    /*
     * Adds many items to the inventory
     * */
    public bool addItem(IItem itemToAdd, int amount)
    {
        if (carryCapacity  > currentWeight + itemToAdd.Weight * amount)
        {
            //if item already in bag
            if (inventory.Contains(itemToAdd))
            {
                inventory[itemToAdd] = inventory[itemToAdd] + amount;
            }
            else // else make a new entry
            {
                inventory.Add(itemToAdd, amount);
            }
            currentWeight = CurrentWeight + itemToAdd.Weight * amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
     * Adds an item to the inventory
     * */
    public bool removeItem(IItem itemToRemove)
    {
        return removeItem(itemToRemove, 1);
    }
    /*
     * Removes many items to the inventory
     * If amount given to remove is greater then the amount 
     * that exists in the bag return false 
     * */
    public bool removeItem(IItem itemToRemove, int amount)
    {
        //if items in bag
        if (inventory.Contains(itemToRemove) && amount <= inventory[itemToRemove])
        {
            if (amount == inventory[itemToRemove])
            {
                inventory.Remove(itemToRemove);
            }
            else
            {
                inventory[itemToRemove] = inventory[itemToRemove] - amount;
            }
            currentWeight = CurrentWeight - itemToRemove.Weight * amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
