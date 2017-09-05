using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private int carryCapacity;
    
    private C5.HashSet<Character> characters;
    private Character currentChar;
    // false for NPC talking, true for dialogue options
    private bool conversationState;
    public bool ConversationState
    {
        get { return conversationState; }
    }

    public TextBoxController textBoxController;
    public TextBoxController inventoryTextController;
    public TextBoxController storeTextController;
    private GameObject itemMenu;
    private GameObject storeMenu;

    
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
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            characters = new C5.HashSet<Character>();
            conversationState = false;

        }
    }
    /*
     * Put everything you want to happen when a scene is loaded here.
     * */
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        itemMenu = GameObject.Find("ItemMenu");
        storeMenu = GameObject.Find("StoreMenu");
        textBoxController = (TextBoxController)GameObject.Find("Text").GetComponent(typeof(TextBoxController));
        storeTextController = (TextBoxController)GameObject.Find("StoreText").GetComponent(typeof(TextBoxController));
        inventoryTextController =  (TextBoxController)GameObject.Find("ItemText").GetComponent(typeof(TextBoxController));
        inventoryTextController.setText(inventory.ToString());
        itemMenu.SetActive(!itemMenu.activeSelf);
        storeMenu.SetActive(!storeMenu.activeSelf);
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
        textBoxController.setText(currentChar.conversationTree.startConversation().Text);
        currentChar.checkAlterCharacter();
        conversationState = false;
    }

    public void displayOptions()
    {
        ArrayList<string> options = currentChar.getOptions();
        string words = "";
        for(int i=1;i<=options.Count;i++)
        {
            words = words + options[i-1] + " ("+i+")\n";
        }
        textBoxController.setText(words);
        conversationState = true;
    }

    public void selectOption(int selection)
    {
        ConversationTreeNode newNode = currentChar.conversationTree.pickOption(selection);
        if (newNode != null)
        {
            textBoxController.setText(newNode.Text);
            currentChar.checkModifyInventory(ref inventory);
            currentChar.checkAlterCharacter();
            conversationState = false;
        }
        else if (currentChar.conversationTree.getCurrentNode().getNewIndex(selection) == -1)
        {
            textBoxController.setText("");
            conversationState = false;
        }
    }

    public void endConversation()
    {
        if (textBoxController.getText().Length != 0)
        {
            textBoxController.setText(currentChar.conversationTree.getLeaveText());
        }
        conversationState = false;
    }

    //end character stuff
    //////////////////////////////
}
