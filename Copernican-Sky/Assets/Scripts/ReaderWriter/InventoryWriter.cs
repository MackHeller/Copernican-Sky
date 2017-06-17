using UnityEngine;
using C5;

public class InventoryWriter
{

    public static void saveAsNewInventory(Inventory inventory, int id)
    {
        string filePath = Application.dataPath + "/JSONFiles/Inventory/SavedState-" + id + "-Inventory.JSON";
        string json = buildJSONFromObject(filePath, inventory, id);
        ReaderWriterUtils.writeFile(filePath, json);
    }

    private static string buildJSONFromObject(string filePath, Inventory inventory, int id)
    {

        string json = "{\"saveStateId\": \"" + id + "\",\n\"carryCapacity\": \"" +
            inventory.CarryCapacity + "\"";

        HashDictionary<IItem, int> list = inventory.InventoryList;
        if (list.Count > 0)
            json = json + ",\n\"items\": [\n";
        foreach (KeyValuePair<IItem, int> entry in list)
            json = json + "{\"id\": \"" + (int)entry.Key.ItemType + "\",\n\"amount\": " + entry.Value + "},\n";
        if (list.Count > 0)
            json = json.Remove(json.Length - 2) + "\n]";
        return json + "\n}";
    }
}
