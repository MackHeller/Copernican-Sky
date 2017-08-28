using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C5;

public class OverWorldController : MonoBehaviour {
    private Inventory inventory;
    public TextBoxController textBoxController;
    public TextBoxController inventoryTextController;
    private int carryCapacity;
    private GameObject itemMenu;
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
            itemMenu = GameObject.Find("InventoryMenu");
        }
    }
    void OnLevelWasLoaded()
    {
        itemMenu = GameObject.Find("InventoryMenu");
    }
    public bool addItemToInventory(IItem itemToAdd, int amount)
    {
        //Set the text to tell the player what item they found
        if (inventory.addItem(itemToAdd, amount)) {
            if (amount > 1)
            {
                textBoxController.setText("You found " + amount + " " + itemToAdd.ItemName + "'s!");
            }
            else
            {
                textBoxController.setText("You found " + amount + " " + itemToAdd.ItemName + "!");
            }
            return true;
        }
        return false;
         
    }
    private void Update()
    {
        //Toggle menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            itemMenu.SetActive(!itemMenu.activeSelf);
        }
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
