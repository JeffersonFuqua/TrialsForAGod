using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenuManager : MonoBehaviour
{
    public GameObject nameMenu;
    public EventSystem ES;
    public GameObject bLetter;
    public void StartGame()
    {
        nameMenu.SetActive(true);
        gameObject.SetActive(false);
        ES.GetComponent<EventSystem>().SetSelectedGameObject(bLetter, null);
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
