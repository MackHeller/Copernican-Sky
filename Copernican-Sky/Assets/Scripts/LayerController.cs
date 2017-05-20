using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour {
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		// we are accessing the SpriteRenderer that is attached to the Gameobject
		spriteRenderer = GetComponent<SpriteRenderer>(); 

	}

	// Update is called once per frame
	void Update () {
		// this ties an object's sort order to its height on the screen
		spriteRenderer.sortingOrder = -(int)(Mathf.Round(transform.position.y*100));

	}
}
