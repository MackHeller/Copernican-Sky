using C5;

public class ConversationTree
{
    ArrayList<ConversationTreeNode> tree;
    private int startIndex;
    private int currentIndex;
    public ConversationTree(string name, int startIndex, ArrayList<ConversationTreeNode> tree)
    {
        this.startIndex = startIndex;
        currentIndex = startIndex;
        this.tree = tree;
    }
    public ConversationTreeNode startConversation()
    {
        currentIndex = startIndex;
        return tree[currentIndex];
    }
    /*
     * pick: a number between 0-3 representing the option the user picked
     * */
    public ConversationTreeNode pickOption(int pick)
    {
        int newIndex = tree[currentIndex].getOptions(pick);
        if (newIndex >= tree[currentIndex].OptionsText.Length)//not pickable
        {
            return tree[currentIndex];
        }
        currentIndex = newIndex;
        return tree[newIndex];
    }
    public void setStartIndex(int index)
    {
        startIndex = index;
    }
}


public class ConversationTreeNode {
    private string text;
    private int[] options;//-1 means end conversation
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

    public string[] OptionsText
    {
        get
        {
            return optionsText;
        }
    }
}
