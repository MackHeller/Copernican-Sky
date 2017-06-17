using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour {

	public Text textbox;
	void Start () {
		textbox.text = "Hello World";
	}
}
