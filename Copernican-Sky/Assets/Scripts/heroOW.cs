using UnityEngine;
using System.Collections;

/*
 * holds functions and properties for the overworld character object. Properties that are character specific can
 * be found in HeroCB.
 * */
public class HeroOW : MonoBehaviour {
    private float speed = 8.0f;
    private float distance = 5f;
    public Rigidbody2D rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        takeStep();
	}

    private void takeStep()
    {
        Vector2 newPos = new Vector2(0.0f,0.0f);
        if (Input.GetKey(KeyCode.W))
            newPos.y = distance;
        if (Input.GetKey(KeyCode.S))
            newPos.y = -distance;
        if (Input.GetKey(KeyCode.A))
            newPos.x = -distance;
        if (Input.GetKey(KeyCode.D))
            newPos.x = distance;
        rb.AddForce(newPos * speed);
    }
}
