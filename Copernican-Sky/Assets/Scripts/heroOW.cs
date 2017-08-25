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
	public Sprite lsprite;
	public Sprite rsprite;
	public Sprite usprite;
	public Sprite dsprite;

	private SpriteRenderer spriteRenderer;

	//variables for tracking 8 directions of movement
	private bool goingup = false;
	private bool goingright = false;
	private bool goingleft = false;
	private bool goingdown = false;

	private float distance = 5f;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = usprite;

    }

	//Note, FixedUpdate is called based on physics frames, indepent of ingame frame rate, which can vary
	//It should be used with any physics engine related functions
	void FixedUpdate(){
		takeStep();
	}

	//Simple movement code, will probably need to be updated at some point
    private void takeStep()
	{
		Vector2 newPos = new Vector2 (0.0f, 0.0f);
		if (Input.GetKey (KeyCode.W)){
			newPos.y = distance;
			goingup = true;
			goingdown = false;
		}
		if (Input.GetKey (KeyCode.S)) {
			newPos.y = -distance;
			goingup = false;
			goingdown = true;
		}
		if (Input.GetKey (KeyCode.A)) {
			newPos.x = -distance;
			goingright = false;
			goingleft = true;
		}
		if (Input.GetKey (KeyCode.D)) {
			newPos.x = distance;
			goingright = true;
			goingleft = false;
		}
		rb.velocity = (newPos * speed);
        //rb.AddForce(newPos * speed);
		if ((rb.velocity).x == 0) {
			goingright = false;
			goingleft = false;
		}
		if ((rb.velocity).y == 0) {
			goingup = false;
			goingdown = false;
		}
		changesprite();
    }

	private void changesprite()
	{
		if (goingup && !goingdown) {
			Debug.Log("u");
			spriteRenderer.sprite = usprite;
			if (goingright && !goingleft)
			{
				//up and right
			}
			else if (!goingright && goingleft)
			{
				//up and left
			}
			else if (!goingright && !goingleft)
			{
				//up
			}
		}
		else if (!goingup && goingdown){
			spriteRenderer.sprite = dsprite;
			if (goingright && !goingleft)
			{
				//down and right
			}
			else if (!goingright && goingleft)
			{
				//down and left
			}
			else if (!goingright && !goingleft)
			{
				//down
			}
		}
		else if (!goingup && !goingdown){
			if (goingright && !goingleft)
			{
				spriteRenderer.sprite = rsprite;
				//right
			}
			else if (!goingright && goingleft)
			{
				spriteRenderer.sprite = lsprite;
				//left
			}
			else if (!goingright && !goingleft)
			{
				//not moving
			}
		}
	}
		
}
