using C5;
using LitJson;
using System;
using UnityEngine;
public static class CharacterReader
{
    public static JsonData openCharacterJSON()
    {
        return ReaderWriterUtils.readFile(Application.dataPath + "/JSONFiles/Character/CharacterStartStates.JSON");
    }

    //default return 0
    public static int getIndexFromJSON(JsonData tree, string name)
    {
        if(ReaderWriterUtils.jsonDataContainsKey(tree, name))
            return Convert.ToInt32(tree[name][0].ToString());
        return 0;
    }
    public static ArrayList<int> getBlackListFromJSON(JsonData tree, string name)
    {
        int len = tree[name][1].Count;
        ArrayList<int> blacklist = new ArrayList<int>();
        for (int j = 0; j < len; j++)
        {
            blacklist.Add(Convert.ToInt32(tree[name][1][j].ToString()));
        }
        return blacklist;
    }

}
