using System;
using C5;
using LitJson;
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
    public ArrayList<int> blacklistIndexes;
    /**
     * check if current state alters the inventory 
     * @param  inventory    the inventory to edit 
     * @return              the new version of the inventory 
     **/
    public virtual void checkModifyInventory(ref Inventory inventory) { }
    public virtual void checkModifyPerks(ref Inventory inventory) { }
    public virtual void checkAlterCharacters(ref C5.HashSet<Character> characters) { }
    public string getName()
    {
        return characterName;
    }
    public static Character createCharacterByName(string name)
    {
        switch (name)
        {
            case "Thomdril Merrilin":
                return new ThomdrilMerrilin();
            case "Padan Fain":
                return new PadanFain();
            case "Cryo Pod":
                return new CryoPod();
            case "Perks":
                return new Perks();
            default:
                throw new Exception("name does not exist");
        }
    }
    public ArrayList<string> getOptions()
    {
        string[] optionsText = conversationTree.getCurrentNode().OptionsText;
        ArrayList<string> newList = new ArrayList<string>();
        for (int i = 0; i < optionsText.Length; i++)
        {
            if (!blacklistIndexes.Contains(conversationTree.getCurrentNode().getNewIndex(i)))
            {
                newList.Add(optionsText[i]);
            }
        }
        return newList;
    }
    public ConversationTreeNode pickOption(int pick)
    {
        return conversationTree.pickOption(adjustPickForBlackList(pick));
    }
    public int adjustPickForBlackList(int pick)
    {
        ConversationTreeNode oldNode = conversationTree.getCurrentNode();
        //pick within range and blacklisted
        for (int i = 0; i <= pick && oldNode.indexInRange(pick); i++)
        {
            if (blacklistIndexes.Contains(oldNode.getNewIndex(i))) {
                pick = pick + 1;
            }
            
        }
        return pick;
    }
    private void fetchData(string name)
    {
        conversationTree = ConversationTreeReader.loadConversationTree(name);
        JsonData json = CharacterReader.openCharacterJSON();
        conversationTree.setStartIndex(CharacterReader.getIndexFromJSON(json, name));
        blacklistIndexes = CharacterReader.getBlackListFromJSON(json, name);
    }
    /**
    * an example character
    * */
    public class ThomdrilMerrilin : Character
    {
        public ThomdrilMerrilin()
        {
            characterName = "Thomdril Merrilin";
            fetchData("ThomdrilMerrilin");
        }

        public override void checkModifyInventory(ref Inventory inventory)
        {
            //get gleeman's cloak
            if(conversationTree.CurrentIndex == 5)
            {
                inventory.addItem(inventory.buildItem("Gleeman Cloak"));
            }
        }
        public override void checkAlterCharacters(ref C5.HashSet<Character> characters)
        {
            //first time you've talked change starting text
            if(conversationTree.CurrentIndex == 0)
            {
                conversationTree.setStartIndex(6);
            }
            //if you ask about padan Fain
            if(conversationTree.CurrentIndex == 8)
            {
                Character fain = findCharacterByType(characters, typeof(PadanFain));
                if (fain != null)
                {
                    fain.blacklistIndexes.Remove(4);
                }
            }
            //got the cloak, can't get it again
            if (conversationTree.CurrentIndex == 5)
            {
                blacklistIndexes.Add(5);
            }
        }
        
    }

    public class CryoPod : Character
    {
        public CryoPod()
        {
            characterName = "Cryo Pod";
            fetchData("CryoPod");
        }
    }

    public class Perks : Character
    {
        public Perks()
        {
            characterName = "Perks Menu";
            fetchData("Perks");
        }

        public override void checkModifyPerks(ref Inventory inventory)
        {
            if (conversationTree.CurrentIndex < 6 && conversationTree.CurrentIndex != 0)
            {
                inventory.setEquipSlot(((EquipSlot)conversationTree.CurrentIndex) - 1, inventory.CurrentlySelected);
            }
        }
    }

    public class PadanFain : Character
    {
        public PadanFain()
        {
            characterName = "Padan Fain";
            fetchData("PadanFain");
        }
        public override void checkAlterCharacters(ref C5.HashSet<Character> characters)
        {
            //first time you've talked indicate that you've met
            if (conversationTree.CurrentIndex == 0)
            {
                Character thom = findCharacterByType(characters, typeof(ThomdrilMerrilin));
                if (thom != null)
                {
                    thom.blacklistIndexes.Remove(8);
                }
            }
            //if you accuse him of being a darkfriend he wont talk to you anymore
            if (conversationTree.CurrentIndex == 4)
            {
                conversationTree.setStartIndex(6);
            }
        }

    }

    public static Character findCharacterByType(C5.HashSet<Character> characters, Type type)
    {
        foreach(Character cha in characters)
        {
            if(cha.GetType().Equals(type))
            {
                return cha;
            }
        }
        return null;
    }
}

