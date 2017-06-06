using C5;
using LitJson;
using System;
using System.IO;
using UnityEngine;
public class ConversationTreeReader {
    public static ConversationTree loadConversationTree(string name)
    {
        return buildTreeFromJSON(readFile(Application.dataPath + "/JSONFiles/ConversationTrees"+name+ ".JSON"));
    }

    private static JsonData readFile(string filePath)
    {
        StreamReader sr = null;
        try
        {
            sr = new StreamReader(filePath);
            return JsonMapper.ToObject(sr.ReadToEnd());
        }
        catch (IOException e)
        {
            Console.WriteLine("Cannot read file");
            Console.WriteLine(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine("Cannot access file");
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (sr != null)
                sr.Dispose();
        }
        return null;
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
