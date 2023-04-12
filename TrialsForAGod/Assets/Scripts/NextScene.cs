using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private int currentScene;
    public GameObject blackScreen;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(fadeInDisable());
    }
    public void ChangeScene()
    {
        StartCoroutine(fadeAnimDelay());
    }

    IEnumerator fadeInDisable()
    {
        blackScreen.GetComponent<Animator>().SetTrigger("fadeIn");
        yield return new WaitForSeconds(1);
        //blackScreen.SetActive(false);
    }
    IEnumerator fadeAnimDelay()
    {
        //blackScreen.SetActive(true);
        blackScreen.GetComponent<Animator>().SetTrigger("fadeOut");
        yield return new WaitForSeconds(1);
        if(SceneManager.sceneCount == currentScene + 1)
        {
            SceneManager.LoadScene(0);

        }
        else
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }
}
