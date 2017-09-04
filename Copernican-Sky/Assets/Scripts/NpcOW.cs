using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOW : MonoBehaviour
{
    private OverWorldController overWorldController;
    private string characterName;
    private bool talking;
    private void Start()
    {
        this.overWorldController = GameObject.Find("GameController").GetComponent<OverWorldController>();
        this.characterName = this.transform.parent.gameObject.name;
        talking = false;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            overWorldController.beginConversation(characterName);
            talking = true;
        }
    }

    void Update()
    {
        if (talking)
        {
            if ((Input.GetKey(KeyCode.E)) && !overWorldController.ConversationState)
            {
                overWorldController.displayOptions();
            }
            else if (overWorldController.ConversationState)
            {
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    overWorldController.selectOption(0);
                }
                else if (Input.GetKey(KeyCode.Alpha2))
                {
                    overWorldController.selectOption(1);
                }
                else if (Input.GetKey(KeyCode.Alpha3))
                {
                    overWorldController.selectOption(2);
                }
                else if (Input.GetKey(KeyCode.Alpha4))
                {
                    overWorldController.selectOption(3);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        overWorldController.endConversation();
        talking = false;
    }

}