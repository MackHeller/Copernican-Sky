using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu_TransitionScene : MonoBehaviour {

    public void LoadNextIndex(string currentButton)
    {
        Debug.Log(currentButton);
        string yup = SceneManager.GetActiveScene().name;
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            Debug.Log("start");
            LoadNextScene("OptionScene", "OptionScene", currentButton);
        }
        else if (SceneManager.GetActiveScene().name == "OptionMenu")
        {
            LoadNextScene("StartMenu", "StartMenu" ,currentButton);
        }

    }
    public void LoadNextScene(string next, string previous, string but)
    {
        if (but == "Right")
        {
            Scene temp = SceneManager.GetSceneByName(next);
            Debug.Log(temp.name);
            if (temp.isLoaded == false)
            {
                SceneManager.LoadScene(temp.name);
            }
            Debug.Log(temp.isLoaded);
            SceneManager.SetActiveScene(temp);

        }
        else if (but == "Left")
        {
            Scene temp = SceneManager.GetSceneByName(previous);
            Debug.Log(temp.isLoaded);
            if (temp.isLoaded == false)
            {
                SceneManager.LoadScene(temp.name);
            }
            SceneManager.SetActiveScene(temp);
        }
    }
}
