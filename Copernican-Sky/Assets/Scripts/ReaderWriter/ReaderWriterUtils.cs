using LitJson;
using System;
using System.IO;
using System.Collections;
using UnityEngine;
public static class ReaderWriterUtils {

    public static JsonData readFile(string filePath)
    {
        StreamReader sr = null;
        try
        {
            sr = new StreamReader(filePath);
            return JsonMapper.ToObject(sr.ReadToEnd());
        }
        catch (IOException e)
        {
            Debug.Log("Cannot read file"+ filePath);
            Console.WriteLine(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            Debug.Log("Cannot access file"+ filePath);
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (sr != null)
                sr.Dispose();
        }
        return null;
    }
    public static bool writeFile(string filePath, string json)
    {
        StreamWriter sw = null;
        try
        {
            sw = new StreamWriter(filePath);
            sw.WriteLine(json);
            return true; 
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        finally
        {
            if (sw != null)
                sw.Dispose();
        }
    }
    public static bool jsonDataContainsKey(JsonData data, string key)
    {
        bool result = false;
        if (data == null)
            return result;
        if (!data.IsObject)
        {
            return result;
        }
        IDictionary tdictionary = data as IDictionary;
        if (tdictionary == null)
            return result;
        if (tdictionary.Contains(key))
        {
            result = true;
        }
        return result;
    }
}
