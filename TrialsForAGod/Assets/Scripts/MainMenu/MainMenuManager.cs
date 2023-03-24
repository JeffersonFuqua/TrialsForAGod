using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenuManager : MonoBehaviour
{
    public GameObject nameMenu;
    public EventSystem ES;
    public GameObject galleryMenu;
    public GameObject optionsMenu;
    public void StartGame()
    {
        nameMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Gallery()
    {
        Debug.Log("gallery");
        galleryMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Options()
    {
        Debug.Log("options");
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
