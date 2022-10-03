using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    PlayerActions pActions;

    bool bIsPaused;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.Pause.started += CallPause;
    }

    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerControls.Dodge.started -= CallPause;
    }

    private void CallPause(InputAction.CallbackContext c)
    {
        //just another way to pause and unpause, the buttons can only call Pause();
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
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
