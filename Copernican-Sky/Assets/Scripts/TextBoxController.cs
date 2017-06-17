using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour {

	private Text textbox;
	void Start () {
		textbox = GetComponent<Text> ();
		textbox.text = "Hello World";
	}

	public void setText(string textinput)
	{
		textbox.text = textinput;
	}
}
