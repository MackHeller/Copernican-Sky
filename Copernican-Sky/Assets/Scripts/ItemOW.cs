using UnityEngine;

public class ItemOW : MonoBehaviour
{
    private OverWorldController overWorldController;
    private SpriteRenderer spriteRenderer;
    public Sprite altSprite;
    public int amount;
    public string itemName;
    public IItem item;
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
                overWorldController.TogglePerks = true;
                overWorldController.addItemToInventory(overWorldController.getItemByName("Mildmannered"), 1);
                overWorldController.addItemToInventory(overWorldController.getItemByName("Wildwoman"), 1);
                overWorldController.TogglePerks = false;
                overWorldController.addItemToInventory(overWorldController.getItemByName(itemName), amount);
            }
        }
    }
}


    