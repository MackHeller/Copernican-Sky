using LitJson;
using System;
using UnityEngine;
public class EnemyReader {
    public static Enemy loadEnemy(string name)
    {
        return buildEnemyFromJSON(ReaderWriterUtils.readFile(Application.dataPath + "/JSONFiles/Enemy/" + name + ".JSON"));
    }
    private static Enemy buildEnemyFromJSON(JsonData tree)
    {
        int[] list = new int[tree["skills"].Count];
        for (int i = 0; i < tree["skills"].Count; i++)
        {
            list[i] = Convert.ToInt32(tree["skills"][i]);
        }
        return new Enemy(tree["name"].ToString(),new Skills(list), (PersonalityTypes)Convert.ToInt32(tree["personality"]));
    }
}
