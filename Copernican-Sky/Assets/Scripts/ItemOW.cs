using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOW : MonoBehaviour
{
    private OverWorldController overWorldController;
    private SpriteRenderer spriteRenderer;
	public TextBoxController textBoxController;
    public Sprite altSprite;
    public int amount;
    public string itemName;
    private void Start()
    {
        overWorldController = GameObject.Find("GameController").GetComponent<OverWorldController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //TODO On contact add a *Item to the *Inventory
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            //if chest is closed open it and get the item
            if(altSprite != spriteRenderer.sprite)
            {
				spriteRenderer.sprite = altSprite;
                overWorldController.addItemToInventory(overWorldController.getItemByName(itemName), amount);

				//Set the text to tell the player what item they found
				if (amount > 1) {
					textBoxController.setText ("You found "+amount+" "+itemName+"'s!");
				} else {
					textBoxController.setText ("You found "+amount+" "+itemName+"!");
				}
            }
        }
    }
}


    