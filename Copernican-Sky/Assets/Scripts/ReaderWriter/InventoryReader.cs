using LitJson;
using System;
using UnityEngine;
public class InventoryReader
{
    public static Inventory loadInventory(int id)
    {
        return buildTreeFromJSON(ReaderWriterUtils.readFile(Application.dataPath + "/JSONFiles/Inventory/SavedState-" + id + "-Inventory.JSON"));
    }
    private static Inventory buildTreeFromJSON(JsonData tree)
    {
        Inventory inventory = new Inventory(Convert.ToInt32(tree["carryCapacity"].ToString()));
        for (int i = 0; i < tree["items"].Count; i++)
        {
            inventory.addItem((inventory.buildItem((string)tree["items"][i]["name"])), 
                Convert.ToInt32(tree["items"][i]["amount"].ToString()));
        }
        return inventory;
    }
}
