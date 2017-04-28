using UnityEngine;
using UnityEngine.SceneManagement;

public class doorOW : MonoBehaviour {
    public int level = 0;
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Mack"); 
        if (collision.gameObject.name == "Hero")
        {
            SceneManager.LoadScene(level);
        }
    }
}
