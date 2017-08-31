using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C5;
using UnityEngine.SceneManagement;

public class OverWorldController : MonoBehaviour {
    private Inventory inventory;
    public TextBoxController textBoxController;
    public TextBoxController inventoryTextController;
    private int carryCapacity;
    private GameObject itemMenu;
    /**
     * put everything you want to happen when the FIRST scene is loaded
     * 
     * */
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
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        }
    }
    /*
     * Put everything you want to happen when a scene is loaded here.
     * 
     * */
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        itemMenu = GameObject.Find("ItemMenu");
        itemMenu.SetActive(!itemMenu.activeSelf);
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
        return IItem.buildItem(name);
    }
}
