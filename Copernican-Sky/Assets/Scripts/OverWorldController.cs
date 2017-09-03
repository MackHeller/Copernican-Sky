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
        textBoxController = (TextBoxController)GameObject.Find("Text").GetComponent(typeof(TextBoxController));
        inventoryTextController =  (TextBoxController)GameObject.Find("ItemText").GetComponent(typeof(TextBoxController));
        inventoryTextController.setText(inventory.ToString());
        itemMenu.SetActive(!itemMenu.activeSelf);
    }
    public void addItemToInventory(IItem itemToAdd, int amount)
    {
        //Set the text to tell the player what item they found
        string text = "";
        if (amount > 1)
        {
            text = "You found " + amount + " " + itemToAdd.ItemName + "'s!\n";
        }
        else
        {
            text = "You found " + amount + " " + itemToAdd.ItemName + "!\n";
        }
        if (!inventory.addItem(itemToAdd, amount)) 
        {
            text = text + "Not enough Room in your inventory!";
        }
        textBoxController.setText(text);

    }
    private void Update()
    {
        //Toggle menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            itemMenu.SetActive(!itemMenu.activeSelf);
            inventoryTextController.setText(inventory.ToString());
        }
    }
    public IItem getItemByName(string name)
    {
        return IItem.buildItem(name);
    }
}
