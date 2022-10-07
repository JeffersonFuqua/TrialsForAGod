using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    PlayerActions pActions;

    bool bIsPaused;
    public GameObject eSystem;
    public GameObject continueButton;


    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.Pause.started += CallPause;

        eSystem.GetComponent<EventSystem>().SetSelectedGameObject(continueButton, null);
    }

    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerControls.Dodge.started -= CallPause;
    }

    private void CallPause(InputAction.CallbackContext c)
    {
        //just another way to pause and unpause, the buttons can only call Pause();
        eSystem.GetComponent<EventSystem>().SetSelectedGameObject(continueButton, null);
        Pause();
    }
    public void Pause()
    {
        if (!bIsPaused)
        {
            pauseMenu.SetActive(true);
            bIsPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            bIsPaused = false;
        }
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

}
