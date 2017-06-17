using System;
/**
* This class is a wrapper for Conversation Tree that handles changes to the rest of the game's systems if 
* we reach a certain state. Each Character will have methods that check if different game systems need to 
* be altered if at certain states. 
* 
* */
public abstract class Character {
    //the conversation tree object
    public  ConversationTree conversationTree;
    /**
     * check if current state alters the inventory 
     * @param  inventory    the inventory to edit 
     * @return              the new version of the inventory 
     **/
    public abstract Inventory checkModifyInventory(Inventory inventory);
}
/**
 * an example character
 * */
public class ThomdrilMerrilin : Character 
{
    public ThomdrilMerrilin()
    {
        conversationTree = ConversationTreeReader.loadConversationTree("ThomdrillMerrilin");
    }

    public override Inventory checkModifyInventory(Inventory inventory)
    {
        throw new NotImplementedException();
    }
}
