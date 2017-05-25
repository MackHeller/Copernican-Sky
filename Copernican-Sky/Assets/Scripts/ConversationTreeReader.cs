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
    private ConversationTree buildTreeFromJSON(JsonData tree)
    {

        ConversationTree conversationTree = new ConversationTree();
        return 
    }
}
