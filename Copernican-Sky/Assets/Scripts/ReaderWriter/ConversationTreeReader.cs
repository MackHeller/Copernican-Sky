using C5;
using LitJson;
using System;
using UnityEngine;
public static class ConversationTreeReader {
    public static ConversationTree loadConversationTree(string name)
    {
        return buildTreeFromJSON(ReaderWriterUtils.readFile(Application.dataPath + "/JSONFiles/ConversationTrees"+name+ ".JSON"));
    }

    
    private static ConversationTree buildTreeFromJSON(JsonData tree)
    {
        ArrayList<ConversationTreeNode> nodes = new ArrayList<ConversationTreeNode>();
        for (int i = 0; i < tree["nodes"].Count; i++)
        {
            int len = tree["nodes"][i]["optId"].Count;
            int[] options = new int[len];//-1 means end conversation
            string[] optionsText = new string[len];
            for(int j=0;j< len;j++)
            {
                options[j] = Convert.ToInt32(tree["nodes"][i]["optId"][j]);
                optionsText[j] = tree["nodes"][i]["optId"][j].ToString();
            }
            nodes[i] = new ConversationTreeNode(tree["nodes"][i]["text"].ToString(),options,optionsText);
        }
        return new ConversationTree(tree["name"].ToString(), Convert.ToInt32(tree["startIndex"]), nodes); 
    }
}
