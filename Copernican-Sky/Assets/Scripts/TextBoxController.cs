using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour {

	//creates an empty variable for the Text object
	private Text textbox;

	void Start () {
		//fetches the Text object from the object the script is attached to
		textbox = GetComponent<Text> ();

		//Example: textbox.text = "Hello World";

		textbox.text = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";
	}

	// a very simple function that other classes can use to set the text onscreen
	public void setText(string textinput)
	{
		textbox.text = textinput;
	}
}
