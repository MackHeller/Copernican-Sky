using C5;

public class ConversationTree
{
    ArrayList<ConversationTreeNode> tree;
    private int startIndex;
    private int currentIndex;
    private ConversationTree(string fileName) : this(fileName, 0) { }
    public ConversationTree(string fileName, int startIndex)
    {
        this.startIndex = startIndex;
        currentIndex = startIndex;
        //TODO use filename to create tree
    }
    public ConversationTreeNode startConversation()
    {
        currentIndex = startIndex;
        return tree[currentIndex];
    }
    /*
     * pick: a number between 1-4 representing the option the user picked
     * */
    public ConversationTreeNode pickOption(int pick)
    {
        int newIndex = tree[currentIndex].getOptions(pick);
        if (newIndex == -1)//not pickable
        {
            return tree[currentIndex];
        }
        return tree[newIndex];
    }
    public void setStartIndex(int index)
    {
        startIndex = index;
    }
}


public class ConversationTreeNode {
    private string text;
    private int[] options;//-1 means not pickable
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
    public int getOptions(int index)
    {
        return options[index];
    }
    public string getOptionsText(int index)
    {
        return optionsText[index];
    }
    
}
