using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("start");
        SceneManager.LoadScene(1);
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
