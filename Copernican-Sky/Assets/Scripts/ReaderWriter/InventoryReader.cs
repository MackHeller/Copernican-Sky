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
        Inventory inventory = new Inventory(Convert.ToInt32(tree["carryCapacity"]));
        for (int i = 0; i < tree["items"].Count; i++)
        {
            inventory.addItem((IItem.buildItem((ItemType)Convert.ToInt32(tree["items"][i]["id"]))), 
                Convert.ToInt32(tree["items"][i]["amount"]));
        }
        return inventory;
    }
}
