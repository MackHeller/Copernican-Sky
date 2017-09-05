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
    private String characterName;
    /**
     * check if current state alters the inventory 
     * @param  inventory    the inventory to edit 
     * @return              the new version of the inventory 
     **/
    public abstract void checkModifyInventory(ref Inventory inventory);
    public abstract string getName();
    public static Character createCharacterByName(string name)
    {
        switch (name)
        {
            case "Thomdril Merrilin":
                return new ThomdrilMerrilin();
            default:
                throw new Exception("name does not exist");
        }
    }
    /**
    * an example character
    * */
    public class ThomdrilMerrilin : Character
    {
        public ThomdrilMerrilin()
        {
            characterName = "Thomdril Merrilin";
            conversationTree = ConversationTreeReader.loadConversationTree("ThomdrilMerrilin");
            conversationTree.setStartIndex(CharacterReader.loadStartIndex("ThomdrilMerrilin"));
        }

        public override void checkModifyInventory(ref Inventory inventory)
        {
            //get gleeman's cloak
            if(conversationTree.CurrentIndex == 5)
            {
                inventory.addItem(IItem.buildItem("Gleeman Cloak"));
            }
        }
        public override string getName()
        {
            return characterName;
        }
    }
}

