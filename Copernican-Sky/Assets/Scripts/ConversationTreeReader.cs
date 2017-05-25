using C5;
using LitJson;
using System;
using System.IO;
using UnityEngine;
public class ConversationTreeReader {
    public static ConversationTree loadConversationTree(string name)
    {
        return reader(Application.dataPath + "/JSONFiles/ConversationTrees");
    }

    private static ConversationTree reader(string filePath)
    {
        StreamReader sr = null;
        try
        {
            sr = new StreamReader(filePath);
            return null;//JsonMapper.ToObject(sr.ReadToEnd());
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
}
