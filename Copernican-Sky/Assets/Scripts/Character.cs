

using System;
/**
* TODO add more modify classes for additional things to modify
* 
* */
public abstract class Character {
    protected  ConversationTree conversationTree;
    public abstract Inventory checkModifyInventory(Inventory inventory);
    
}
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
