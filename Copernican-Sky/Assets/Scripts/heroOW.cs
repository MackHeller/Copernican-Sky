using UnityEngine;
using System.Collections;

/*
 * holds functions and properties for the overworld character object. Properties that are character specific can
 * be found in HeroCB.
 * */
public class heroOW : MonoBehaviour {
	//Public variables can be changed/set from within the Unity Editor
    public float speed = 20f;
    public Rigidbody2D rb;

	private float distance = 5f;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

	//Note, FixedUpdate is called based on physics frames, indepent of ingame frame rate, which can vary
	//It should be used with any physics engine related functions
	void FixedUpdate(){
		takeStep();
	}

	//Simple movement code, will probably need to be updated at some point
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
		rb.velocity = (newPos * speed);
        //rb.AddForce(newPos * speed);
    }
		
}
