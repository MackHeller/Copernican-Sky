using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour {

public void LoadSecondaryMenu(GameObject SubMenu)
    {
        SubMenu.SetActive(true);
    }
public void CloseSecondaryMenu(GameObject SubMenu)
    {
        SubMenu.SetActive(false);
    }
}
