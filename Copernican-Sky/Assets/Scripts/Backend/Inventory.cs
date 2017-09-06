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
    //how much you can carry
    private int carryCapacity;
    //how much you are carrying
    private double currentWeight;

    public Inventory(int carryCapacity)
    {
        inventory = new HashDictionary<IItem, int>();
        this.carryCapacity = carryCapacity;
        currentWeight = 0.0;
    }
    //getter methods 
    public double CurrentWeight
    {
        get { return currentWeight; }
    }

    public HashDictionary<IItem, int> InventoryList
    {
        get{return inventory;}
    }

    public int CarryCapacity
    {
        get
        {
            return carryCapacity;
        }
    }

    /*
    * Adds an item to the inventory
    * @param    itemToAdd   item to add to inventory
    * @return               true if the item was added else false
    * */
    public bool addItem(IItem itemToAdd)
    {
        return addItem(itemToAdd, 1);
    }
    /*
     * Adds many items to the inventory
     * @param    itemToAdd   item to add to inventory
     * @param    amount      amount of the item to add
     * @return               true if the item was added else false
     * */
    public bool addItem(IItem itemToAdd, int amount)
    {
        if (carryCapacity  >= currentWeight + itemToAdd.Weight * amount)
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
     * Removes an item to the inventory
     * @param    itemToRemove   item to remove to inventory
     * @return                  true if the item was removed else false
     * */
    public bool removeItem(IItem itemToRemove)
    {
        return removeItem(itemToRemove, 1);
    }
    /*
     * Removes many items to the inventory
     * If amount given to remove is greater then the amount that exists in the bag return false 
     * @param    itemToRemove   item to add to inventory
     * @param    amount         amount of the item to add
     * @return                  true if the item was added else false
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

    public bool exchangeItem(IItem itemToAdd, int amountToAdd, IItem itemToRemove, int amountToRemove)
    {
        if (removeItem(itemToRemove,amountToRemove))
        {
            return addItem(itemToAdd, amountToAdd);//try to add the item
        }
        else
        {
            addItem(itemToRemove,amountToRemove);//restore item you removed
            return false;
        }
    }

    public override string ToString()
    {
        string words = "";
        foreach (KeyValuePair<IItem, int> item in inventory)
        {
            words = words + item.Key.ToString()+"   "+item.Value+"\n";
        }
        return words;
    }
}
