using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    double weight;
    string itemName;
    public Item(double weight)
    {
        this.weight = weight;

    }
}

public class ItemType
{
    public static readonly Item SWORD = new Item(3.0);
}
