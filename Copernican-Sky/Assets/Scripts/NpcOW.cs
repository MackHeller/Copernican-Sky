using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOW : MonoBehaviour
{
    private OverWorldController overWorldController;
    private NpcMovement npcMovement;
    private string characterName;
    private bool talking;
    private void Start()
    {
        this.overWorldController = GameObject.Find("GameController").GetComponent<OverWorldController>();
        this.characterName = this.transform.parent.gameObject.name;
        npcMovement = (NpcMovement)this.transform.parent.gameObject.GetComponent(typeof(NpcMovement));
        talking = false;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            overWorldController.beginConversation(characterName);
            talking = true;
            npcMovement.setMoving(false);
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
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    overWorldController.selectOption(0);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    overWorldController.selectOption(1);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    overWorldController.selectOption(2);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    overWorldController.selectOption(3);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    overWorldController.selectOption(4);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    overWorldController.selectOption(5);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    overWorldController.selectOption(6);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    overWorldController.selectOption(7);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    overWorldController.selectOption(8);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    overWorldController.selectOption(9);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        overWorldController.endConversation();
        talking = false;
        npcMovement.setMoving(true);
    }

}