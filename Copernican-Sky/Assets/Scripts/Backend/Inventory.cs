using C5;
using UnityEngine;


/**
 * inventory class. Holds items that the player collects overtime
 * items are stored in a HashDictionary where the key is the item 
 * and the value is the amount of that item that the player has
 * foreach(Item key in inventory.Keys)
 * */
public class Inventory {
    //the item and its amount
    private HashDictionary<IItem, int> inventory;
    private HashDictionary<EquipSlot, IItem> equiped;
    //how much you can carry
    private int carryCapacity;
    //how much you are carrying
    private double currentWeight;
    public bool currentlySelected;
    public Inventory(int carryCapacity)
    {
        inventory = new HashDictionary<IItem, int>();
        equiped = new HashDictionary<EquipSlot, IItem>();
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

    public IItem getEquipSlot(EquipSlot slot)
    {
        //if you have an equipment in that slot and you own that equipment
        if (slotEquiped(slot)) {
            if (hasItem(equiped[slot]) != null && inventory[equiped[slot]] > 0)
            {
                return equiped[slot];
            }
            //if you have that item equiped and you dont own the item 
            emptyEquipSlot(slot);
        }
        return null;
    }
    public bool emptyEquipSlot(EquipSlot slot)
    {
        //if items in bag
        if (slotEquiped(slot))
        {
            equiped.Remove(slot);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool setEquipSlot(IItem item)
    {
        return setEquipSlot((EquipSlot)item.getIntValue("equipSlot"), item);
    }
    public bool setEquipSlot(EquipSlot slot, IItem item)
    {
        //if the item goes in that slot and you have more then 1 of that item
        if ((EquipSlot)item.getIntValue("equipSlot") == slot && hasItem(equiped[slot]) != null && inventory[equiped[slot]] > 0)
        {
            equiped[slot] = item;
            return true;
        }
        return false;
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
            if (hasItem(itemToAdd)!=null)
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
        if (hasItem(itemToRemove)!=null && amount <= inventory[itemToRemove])
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
        if (removeItem(itemToRemove, amountToRemove))
        {
            if (addItem(itemToAdd, amountToAdd))//try to add the item
            {
                return true;
            }
            addItem(itemToRemove, amountToRemove);//restore item you removed
        }
        return false;
    }

    public IItem hasItem(IItem item)
    {
        foreach(IItem i in inventory.Keys)
        {
            if(i.ItemName == item.ItemName)
            {
                return i;
            }
        }
        return null;
    }
    public bool slotEquiped(EquipSlot slot)
    {
        foreach (EquipSlot i in equiped.Keys)
        {
            if (i == slot)
            {
                return true;
            }
        }
        return false;
    }

    public IItem buildItem(string itemName)
    {
        IItem newItem = IItem.buildItem(itemName);
        IItem item = hasItem(newItem);
        if(item == null)
        {
            return newItem; 
        }
        return item;
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

    public string ToStringInventory()
    {
        string words = "";
        foreach (KeyValuePair<EquipSlot,IItem> item in equiped)
        {
            words = words + item.Key.ToString() + "   " + item.Value + "\n";
        }
        return words;
    }
}
