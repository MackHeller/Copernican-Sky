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
    //TODO On contact start conversation
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("boom");
        if (collision.gameObject.name == "Hero")
        {
            Debug.Log("hero");
            overWorldController.beginConversation(characterName);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //overWorldController.
    }

}