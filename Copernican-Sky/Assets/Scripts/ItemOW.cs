﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOW : MonoBehaviour
{
    public OverWorldController overWorldController;
    public int amount;
    public string itemName;
    //TODO On contact add a *Item to the *Inventory
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            overWorldController.addItemToInventory(overWorldController.getItemByName(itemName), amount);
            Object.Destroy(this.gameObject);
        }
    }
}


    