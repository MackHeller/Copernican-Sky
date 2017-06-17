using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C5;

public class OverWorldController : MonoBehaviour {
    private Inventory inventory;
    private int carryCapacity;
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            //TODO non-dummy value
            carryCapacity = 10;
            inventory = new Inventory(carryCapacity);
        }
    }
    public bool addItemToInventory(IItem itemToAdd, int amount)
    {
        return inventory.addItem(itemToAdd, amount);
    }
    public IItem getItemByName(string name)
    {
        switch (name){
            case "wooden_sword":
                return IItem.buildItem(Item.SWORD_BASIC); 
            default:
                throw new System.Exception();
        }
    }
}
