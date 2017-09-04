using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOW : MonoBehaviour
{
    private OverWorldController overWorldController;
    private string characterName;
    private void Start()
    {
        this.overWorldController = GameObject.Find("GameController").GetComponent<OverWorldController>();
        this.characterName = this.transform.parent.gameObject.name;

    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            overWorldController.beginConversation(characterName);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero") {
            if ((Input.GetKey(KeyCode.E)) && !overWorldController.ConversationState)
            {
                overWorldController.displayOptions();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        overWorldController.endConversation();
    }

}