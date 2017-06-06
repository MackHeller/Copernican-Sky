using C5;
using LitJson;
using System;
using System.IO;
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
}
}
