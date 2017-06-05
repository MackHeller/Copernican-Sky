/**
 * TODO: are Characters just Conversation Trees. IN that case should this class just be for checking if 
 * anything happens when the Tree reaches a certain state?
 * 
 * */

public abstract class Character {
    protected  ConversationTree conversationTree;
    //public abstract int checkState(int state);
    
}
public class ThomdrilMerrilin : Character 
{
    public ThomdrilMerrilin()
    {
        conversationTree = ConversationTreeReader.loadConversationTree("ThomdrillMerrilin");
    }
}
