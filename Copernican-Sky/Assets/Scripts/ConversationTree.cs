using C5;

public class ConversationTree
{
    ArrayList<ConversationTreeNode> tree;
    int startIndex;
    int currentIndex;
    private ConversationTree(string fileName) : this(fileName, 0) { }
    public ConversationTree(string fileName, int startIndex)
    {
        this.startIndex = startIndex;
        currentIndex = startIndex;
        //TODO use filename to create tree
    }
}


public class ConversationTreeNode {
    private string text;
    private int[] options;
    private string[] optionsText;
    public ConversationTreeNode()
    {
        
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
