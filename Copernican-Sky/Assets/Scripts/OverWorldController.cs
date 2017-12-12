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
    private bool buyingAnItem;
    private bool inConversation;
    public bool BuyingAnItem
    {
        get { return buyingAnItem; }
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
        equipMenu = GameObject.Find("EquipMenu");
        storeMenu = GameObject.Find("StoreMenu");
		nameMenu = GameObject.Find("NameMenu");

        equipBoxController = (TextBoxController)GameObject.Find("EquipText").GetComponent(typeof(TextBoxController));
        textBoxController = (TextBoxController)GameObject.Find("MainBoxText").GetComponent(typeof(TextBoxController));
        storeTextController = (TextBoxController)GameObject.Find("StoreText").GetComponent(typeof(TextBoxController));
        inventoryTextController =  (TextBoxController)GameObject.Find("ItemText").GetComponent(typeof(TextBoxController));
		nameTextController =  (TextBoxController)GameObject.Find("NameText").GetComponent(typeof(TextBoxController));

        inventoryTextController.setText(inventory.ToString());
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
        if (!inventory.addItem(itemToAdd, amount)) 
        {
            text = text + "Not enough Room in your inventory!";
        }
        textBoxController.setText(text);

    }
    private void Update()
    {
        //Toggle menu
        if (Input.GetKeyDown(KeyCode.Tab) && !inConversation)
        {
            itemMenu.SetActive(!itemMenu.activeSelf);
            equipMenu.SetActive(!equipMenu.activeSelf);
            inventoryTextController.setText(inventory.ToString());
            equipBoxController.setText(inventory.ToStringInventory());
        }
    }
    public IItem getItemByName(string name)
    {
        return inventory.buildItem(name);
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

        textBoxController.setText(currentChar.conversationTree.startConversation().Text);
        currentChar.checkAlterCharacters(ref characters);

        //close item menus and let other systems know your in  a convo
        conversationState = false;
        itemMenu.SetActive(false);
        equipMenu.SetActive(false);
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
            for (int i = 1; i < options.Count; i++)
            {
                IItem shopItem = inventory.buildItem(options[i - 1]);
                words = words + shopItem.ItemName + " " + shopItem.Weight+"kg " + shopItem.BuyPrice +"g (" + i + ")\n";
            }
            words = words + options.Last + " (" + options.Count + ")\n";
            storeMenu.SetActive(true);
            storeTextController.setText(words);
            buyingAnItem = true;
            itemMenu.SetActive(true);
            inventoryTextController.setText(inventory.ToString());
        }
        else//A conversation
        {
            for (int i = 1; i <= options.Count; i++)
            {
                words = words + options[i - 1] + " (" + i + ")\n";
            }
            textBoxController.setText(words);
        }
        conversationState = true;
    }

    public void selectOption(int pick)
    {
        pick = currentChar.adjustPickForBlackList(pick);
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
                currentChar.checkAlterCharacters(ref characters);
                conversationCleanUp();
				//make the characters name pop up
				nameMenu.SetActive(true);
            }
            else if (currentNode.indexInRange(pick) && currentNode.getNewIndex(pick) == -1)//if not valid because it's the end
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
                inventoryTextController.setText(inventory.ToString());

            }
            else
            {
                //done shopping
                textBoxController.setText(newNode.Text);
                currentChar.checkModifyInventory(ref inventory);
                currentChar.checkAlterCharacters(ref characters);
                conversationCleanUp();
				//make the characters name pop up
				nameMenu.SetActive(true);
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
        itemMenu.SetActive(false);
        equipMenu.SetActive(false);
        nameMenu.SetActive(false);
        conversationState = false;
        buyingAnItem = false;
        inConversation = false;
    }

    //end character stuff
    //////////////////////////////
}
