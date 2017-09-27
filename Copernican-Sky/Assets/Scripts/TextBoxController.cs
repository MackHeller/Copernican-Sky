using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour {

	//creates an empty variable for the Text object
	private Text textbox;

	void Awake () {
		//fetches the Text object from the object the script is attached to
		this.textbox = GetComponent<Text> ();
        string words = "";

        this.setText(words);
    }
    private void Start()
    {
        
    }

    // a very simple function that other classes can use to set the text onscreen
    public void setText(string textinput)
	{
		this.textbox.text = textinput;
	}

    public string getText()
    {
        return textbox.text;
    }
}
