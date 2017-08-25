using UnityEngine;
using UnityEngine.SceneManagement;

public class doorOW : MonoBehaviour {
	
    public string sceneToTravelTo = "";

	//This method runs whenever 2D collision occurs with the object this script is attached to
	void OnCollisionEnter2D(Collision2D collision)
    {
		//Checks if the colliding object is the player
        if (collision.gameObject.name == "Hero")
        {
			//Travel to the scene (set in the unity editor)
            SceneManager.LoadScene(sceneToTravelTo, LoadSceneMode.Single);
        }
    }
    
}
