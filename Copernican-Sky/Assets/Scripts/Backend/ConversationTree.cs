using C5;
/**
 * Handles the conversations for a Character. Structured like a tree where each node is a ConversationTreeNode.
 * Conversations start at the start index and progress as you select dialogue options.
 * When a conversation ends the currentIndex functionally returns to the start index. 
 * You can:
 * -    start a conversation
 * -    pick a dialogue option
 * -    set the start index (where conversations start)
 * -    get the current index 
 **/
public class ConversationTree
{
    //list of nodes
    private ArrayList<ConversationTreeNode> tree;
    //where conversations start
    private int startIndex;
    //the current point in the conversation
    private int currentIndex;
    //if you leave
    private string leaveText;
    public ConversationTree(string name, int startIndex, ArrayList<ConversationTreeNode> tree, string leaveText)
    {
        this.startIndex = startIndex;
        currentIndex = startIndex;
        this.tree = tree;
        this.leaveText = leaveText;
    }
    /**
     * begin a converstion.
     * @return      the first ConversationTreeNode in the conversation
     * */
    public ConversationTreeNode startConversation()
    {
        currentIndex = startIndex;
        return tree[currentIndex];
    }
    /*
     * pick a path in the ConversationTree to travel next to. Returns the new location in the tree.
     * @param   pick    a number between 0-3 representing the option the user picked
     * @return          the new ConversationTreeNode based on what option you picked
     * */
    public ConversationTreeNode pickOption(int pick)
    {
        int newIndex = tree[currentIndex].getNewIndex(pick);
        if (newIndex >= tree[currentIndex].OptionsText.Length)//not pickable
        {
            return tree[currentIndex];
        }
        currentIndex = newIndex;
        return tree[newIndex];
    }
    /**
     * sets the index where conversations start from
     * @param   index   the new starting location
     * */
    public void setStartIndex(int index)
    {
        startIndex = index;
    }
    /*
     * gets the current ConversationTreeNode
     * @return          current location in the tree
     * */
    public ConversationTreeNode getCurrentNode()
    {
        return tree[currentIndex];
    }

    public string getLeaveText()
    {
        return leaveText;
    }
}


public class ConversationTreeNode {
    //what the character says
    private string text;
    //the possible locations in the tree to travel to
    private int[] options;//-1 means end conversation
    //the text associsated with getting to those locations 
    private string[] optionsText;
    public ConversationTreeNode(string text, int[] options, string[] optionsText)
    {
        this.text = text;
        this.options = options;
        this.optionsText = optionsText;
    }
    //getters
    public string Text
    {
        get
        {
            return text;
        }
    }
    public int getNewIndex(int index)
    {
        return options[index];
    }
    public string getOptionsText(int index)
    {
        return optionsText[index];
    }
    public string[] OptionsText
    {
        get
        {
            return optionsText;
        }
    }
}
