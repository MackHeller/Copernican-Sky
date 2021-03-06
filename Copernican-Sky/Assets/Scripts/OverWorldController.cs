﻿using UnityEngine;
using C5;
using UnityEngine.SceneManagement;
/// <summary>
/// Connects the frontend to the backend
/// Sections should all be clearly labeled and in order:
/// 1. What to do on start up and room enter 
/// 2. Item/inventory methods
/// 3. Character methods
/// </summary>
public class OverWorldController : MonoBehaviour {
    private Inventory inventory;
    private Inventory perks;
    private int carryCapacity;
    private int perkEquipLimit;
    
    private C5.HashSet<Character> characters;
    private Character currentChar;
    // false for NPC talking, true for dialogue options
    private bool conversationState;
    public bool ConversationState
    {
        get { return conversationState; }
    }
    private bool buyingAnItem;
    private bool inConversation;
    public bool BuyingAnItem
    {
        get { return buyingAnItem; }
    }

    public bool IsPaused
    {
        get
        {
            return isPaused;
        }
    }

    public bool InInventory
    {
        get
        {
            return inInventory;
        }
    }

    public bool TogglePerks
    {
        get
        {
            return togglePerks;
        }

        set
        {
            togglePerks = value;
        }
    }

    public bool InConversation
    {
        get
        {
            return inConversation;
        }
    }

    public TextBoxController textBoxController;
    public TextBoxController inventoryTextController;
    public TextBoxController storeTextController;
	public TextBoxController nameTextController;
    public TextBoxController equipBoxController;
    private GameObject itemMenu;
    private GameObject equipMenu;
    private GameObject storeMenu;
	private GameObject nameMenu;

    private bool isPaused;
    private bool inInventory;
    private bool togglePerks;
    /**
* put everything you want to happen when the FIRST scene is loaded
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
            perks = new Inventory(carryCapacity);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            characters = new C5.HashSet<Character>();
            conversationState = false;
            isPaused = false;
            inInventory = false;
            togglePerks = false;
        }
    }
    /*
     * Put everything you want to happen when a scene is loaded here.
     * */
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        itemMenu = GameObject.Find("ItemMenu");
        equipMenu = GameObject.Find("EquipMenu");
        storeMenu = GameObject.Find("StoreMenu");
		nameMenu = GameObject.Find("NameMenu");

        equipBoxController = (TextBoxController)GameObject.Find("EquipText").GetComponent(typeof(TextBoxController));
        textBoxController = (TextBoxController)GameObject.Find("MainBoxText").GetComponent(typeof(TextBoxController));
        storeTextController = (TextBoxController)GameObject.Find("StoreText").GetComponent(typeof(TextBoxController));
        inventoryTextController =  (TextBoxController)GameObject.Find("ItemText").GetComponent(typeof(TextBoxController));
		nameTextController =  (TextBoxController)GameObject.Find("NameText").GetComponent(typeof(TextBoxController));

        updateInventoryField();
        itemMenu.SetActive(!itemMenu.activeSelf);
        equipMenu.SetActive(!equipMenu.activeSelf);
        storeMenu.SetActive(!storeMenu.activeSelf);
		nameMenu.SetActive(!nameMenu.activeSelf);
    }

    //////////////////////////////
    //item stuff

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
        if (!getCurrentInventory().addItem(itemToAdd, amount)) 
        {
            text = text + "Not enough Room in your inventory!";
        }
        textBoxController.setText(text);

    }
    private void Update()
    {
        //Toggle menu
        if (Input.GetKeyDown(KeyCode.Tab) && !buyingAnItem)
        {
            itemMenu.SetActive(!itemMenu.activeSelf);
            equipMenu.SetActive(!equipMenu.activeSelf);
            flipInInventoryStatus();
        }
    }
    public IItem getItemByName(string name)
    {
        return inventory.buildItem(name);
    }
    public void flipInInventoryStatus()
    {
        updateInventoryField();
        isPaused = !isPaused;
        inInventory = !inInventory;
    }
    public Inventory getCurrentInventory()
    {
        if (togglePerks)
        {
            return perks;
        }else
        {
            return inventory;
        }
    }
    public void updateInventoryField()
    {
            inventoryTextController.setText(getCurrentInventory().ToString());
            equipBoxController.setText(getCurrentInventory().ToStringEquip());
    }

    public void moveItemSelectUp()
    {
        getCurrentInventory().CurrentlySelected = getCurrentInventory().getPreviousItem(getCurrentInventory().CurrentlySelected);
    }
    public void moveItemSelectDown()
    {
        getCurrentInventory().CurrentlySelected = getCurrentInventory().getNextItem(getCurrentInventory().CurrentlySelected);
    }
    public void equipItem()
    {
        getCurrentInventory().setEquipSlot(getCurrentInventory().CurrentlySelected);
        equipBoxController.setText(getCurrentInventory().ToStringEquip());
    }

    //end item stuff
    //////////////////////////////

    //////////////////////////////
    //character stuff

    public Character getCharacterOrAdd(string characterName)
    {
        foreach (Character ch in characters)
        {
            if (ch.getName() == characterName)
            {
                return ch;
            }
        }
        Character newChar = Character.createCharacterByName(characterName);
        characters.Add(newChar);
        return newChar;
    }

    public void beginConversation(string characterName)
    {
        currentChar = getCharacterOrAdd(characterName);
		//make the characters name pop up
		nameMenu.SetActive(true);
		nameTextController.setText (characterName);
        //not looking at perks
        togglePerks = false;
        textBoxController.setText(currentChar.conversationTree.startConversation().Text);
        currentChar.checkAlterCharacters(ref characters);

        //close item menus and let other systems know your in  a convo
        conversationState = false;
        //set talking person to ai
        inConversation = true;
    }

    public void displayOptions()
    {
        ArrayList<string> options = currentChar.getOptions();
        string words = "";

		//make the characters name pop up
		nameMenu.SetActive(true);

        //check if this is a store menu
        if (currentChar.conversationTree.isStoreMenu())
        {
            itemMenu.SetActive(false);
            equipMenu.SetActive(false);
            for (int i = 1; i < options.Count; i++)
            {
                IItem shopItem = inventory.buildItem(options[i - 1]);
                words = words + "(" + i + ") "+ shopItem.ItemName + " " + shopItem.Weight+"kg " + shopItem.BuyPrice +"g \n";
            }
            words = words + "(" + options.Count + ") "+ options.Last+"\n";
            storeMenu.SetActive(true);
            storeTextController.setText(words);
            buyingAnItem = true;
            itemMenu.SetActive(true);
            updateInventoryField();
        }
        else//A conversation
        {
            for (int i = 1; i <= options.Count; i++)
            {
                words = words + "(" + i + ") "+options[i - 1] + "\n";
            }
            textBoxController.setText(words);
        }
        conversationState = true;
    }

    public void selectOption(int pick)
    {
        if (buyingAnItem)//got to the item buying method
        {
            buyItem(pick);
        }
        else
        {
            ConversationTreeNode currentNode = currentChar.conversationTree.getCurrentNode();
            ConversationTreeNode newNode = currentChar.pickOption(pick);
            if (newNode != null)//if it is a valid new conversation point
            {
                textBoxController.setText(newNode.Text);
                currentChar.checkModifyInventory(ref inventory);
                currentChar.checkModifyPerks(ref perks);
                currentChar.checkAlterCharacters(ref characters);
                conversationState = false;
            }
            else if (currentNode.indexInRange(currentChar.adjustPickForBlackList(pick)) 
                && currentNode.getNewIndex(currentChar.adjustPickForBlackList(pick)) == -1)//if not valid because it's the end
            {
                textBoxController.setText("");
                currentChar.checkModifyInventory(ref inventory);
                currentChar.checkAlterCharacters(ref characters);
                conversationCleanUp();
            }
        }
    }

    private void buyItem(int pick)
    {
        int oldIndex = currentChar.conversationTree.CurrentIndex;
        ConversationTreeNode newNode = currentChar.pickOption(pick);
        if (newNode != null)//if it is a valid new conversation point
        {
            if (oldIndex == currentChar.conversationTree.CurrentIndex)//if selected an item
            {
                IItem itemToAdd = inventory.buildItem(currentChar.conversationTree.getCurrentNode().OptionsText[pick]);
                if (inventory.exchangeItem(itemToAdd, 1, inventory.buildItem("Coin"), itemToAdd.BuyPrice))
                {
                    //we bought it 
                    textBoxController.setText("You have bought: " + itemToAdd.ItemName);
                }
                else
                {
                    //we could not buy it
                    textBoxController.setText("You could not buy " + itemToAdd.ItemName);
                }
                currentChar.checkModifyInventory(ref inventory);
                currentChar.checkAlterCharacters(ref characters);
                updateInventoryField();

            }
            else
            {
                //done shopping
                textBoxController.setText(newNode.Text);
                currentChar.checkModifyInventory(ref inventory);
                currentChar.checkAlterCharacters(ref characters);
                storeMenu.SetActive(false);
                itemMenu.SetActive(false);
                equipMenu.SetActive(false);
                buyingAnItem = false;
                isPaused = false;
                inInventory = false;
                conversationState = false;
            }
        }
    }

    public void endConversation()
    {
        if (textBoxController.getText().Length != 0)
        {
            textBoxController.setText(currentChar.conversationTree.getLeaveText());
        }
        conversationCleanUp();
    }

    public void conversationCleanUp()
    {
        storeMenu.SetActive(false);
        nameMenu.SetActive(false);
        conversationState = false;
        buyingAnItem = false;
        inConversation = false;
    }

    //end character stuff
    //////////////////////////////
}
