using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu_TransitionScene : MonoBehaviour {

    public void LoadNextName(string aScene)
    {
        //Loads the scene by its name
        SceneManager.LoadScene(aScene);
    }

    public void LoadNextIndex(int index)
    {
        //Loads the scene by its buildIndex
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(index).name);
    }

    public void unloadAllScenes()
    {
        //Loops through the list of all loaded scene index, and unloads them
        int x = SceneManager.sceneCount;
        int y = 0;
        while (y < x)
        {
            SceneManager.UnloadSceneAsync(y);
            y = y + 1;
        }
        
    }
}
