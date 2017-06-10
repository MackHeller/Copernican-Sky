using LitJson;
using System;
using UnityEngine;
public class EnemyReader {
    public static EnemyCB loadEnemy(string name)
    {
        return buildEnemyFromJSON(ReaderWriterUtils.readFile(Application.dataPath + "/JSONFiles/Enemy/" + name + ".JSON"));
    }
    private static EnemyCB buildEnemyFromJSON(JsonData tree)
    {
        int[] list = new int[tree["skills"].Count];
        for (int i = 0; i < tree["skills"].Count; i++)
        {
            list[i] = Convert.ToInt32(tree["skills"][i]);
        }
        return new EnemyCB(tree["name"].ToString(),new Skills(list), (PersonalityTypes)Convert.ToInt32(tree["personality"]));
    }
}
