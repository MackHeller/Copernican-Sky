using C5;
using LitJson;
using System;
using UnityEngine;
public static class CharacterWriter
{
    public static int loadStartIndex(string name)
    {
        return getIndexFromJSON(ReaderWriterUtils.readFile(Application.dataPath + "/JSONFiles/Character/CharacterStartStates.JSON"), name);
    }

    //default return 0
    private static int getIndexFromJSON(JsonData tree, string name)
    {
        if(ReaderWriterUtils.jsonDataContainsKey(tree, name))
            return Convert.ToInt32(tree[name]);
        return 0;
    }

}
