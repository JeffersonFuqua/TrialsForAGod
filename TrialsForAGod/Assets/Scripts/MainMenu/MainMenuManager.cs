using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public GameObject nameMenu;
    public void StartGame()
    {
        nameMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Gallary()
    {
        Debug.Log("gallary");
    }
    public void Options()
    {
        Debug.Log("options");
    }
    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
