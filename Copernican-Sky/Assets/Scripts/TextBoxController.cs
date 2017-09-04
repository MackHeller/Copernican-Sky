﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour {

	//creates an empty variable for the Text object
	private Text textbox;

	void Awake () {
		//fetches the Text object from the object the script is attached to
		this.textbox = GetComponent<Text> ();
        string words = "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
                       "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
            "This box can fit 40 W's & M's per line, generally the biggest Alnum chars in most fonts. This is a monospaced font, so all char's are same sized. There are 7 lines of space (280 chars total).";

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
