using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    public OptionsMaster optionsVal;
    public GameObject mainMenu;
    public UIDocument uiDoc;
    private VisualElement rootElement;

    private static Slider masterVolSlider;
    private static Slider musicVolSlider;
    private static Slider sFXVolSlider;

    private Toggle fullscreen;
    private bool bToggledFull;

    private DropdownField resolution;
    private bool bResoVal;
    private int resoVal = 0;
    private int x, y;

    private Button back;

    public EventSystem eS;
    public GameObject startButton;

    private void OnEnable()
    {
        rootElement = uiDoc.rootVisualElement;
        masterVolSlider = rootElement.Q<Slider>("masterVolumeSlider");
        musicVolSlider = rootElement.Q<Slider>("musicVolumeSlider");
        sFXVolSlider = rootElement.Q<Slider>("sFXVolumeSlider");

        fullscreen = rootElement.Q<Toggle>("fullscreen");
        resolution = rootElement.Q<DropdownField>("resolution");

        back = rootElement.Q<Button>("return");
        back.clicked += () => Return();

        masterVolSlider.value = optionsVal.masterVol;
        musicVolSlider.value = optionsVal.musicVol;
        sFXVolSlider.value = optionsVal.sFXVol;

        fullscreen.value = optionsVal.fullscreen;
        resolution.index = optionsVal.resoValue;

        masterVolSlider.Focus();
    }
    private void OnDisable()
    {
        back.clicked -= () => Return();
        
    }

    private void Update()
    {
        optionsVal.masterVol = masterVolSlider.value;
        optionsVal.musicVol = masterVolSlider.value * musicVolSlider.value;
        optionsVal.sFXVol = masterVolSlider.value * sFXVolSlider.value;

            //reso val prevents it from being updated each frame
            if (resoVal != resolution.index)
        {
            resoVal = resolution.index;
            bResoVal = false;
        }

        //1980x1080
        if(resolution.index == 0 && !bResoVal)
        {
            Debug.Log("1980x1080");
            bResoVal = true;
            x = 1980;
            y = 1080;
            FullscreenAndResolution(x, y, fullscreen.value);
        }
        //1280x720
        else if(resolution.index == 1 && !bResoVal)
        {
            Debug.Log("1280x720");
            bResoVal = true;
            x = 1280;
            y = 720;
            FullscreenAndResolution(x, y, fullscreen.value);
        }
        //640x480
        else if(resolution.index == 2 && !bResoVal)
        {
            Debug.Log("640x480");
            bResoVal = true;
            x = 640;
            y = 480;
            FullscreenAndResolution(x, y, fullscreen.value);
        }

        //btoggled prevents it from being updated every frame
        if (fullscreen.value == true && !bToggledFull)
        {
            Debug.Log("full");
            bToggledFull = true;
            FullscreenAndResolution(x, y, fullscreen.value);
        }
        else if (fullscreen.value == false && bToggledFull)
        {
            Debug.Log("fullOff");
            bToggledFull = false;
            FullscreenAndResolution(x, y, fullscreen.value);
        }
    }

    void FullscreenAndResolution(int x, int y, bool ifFullscreen)
    { 
        //sets screen settings
        Screen.SetResolution(x, y, ifFullscreen);
        optionsVal.fullscreen = fullscreen.value;
        optionsVal.resoValue = resolution.index;
        masterVolSlider.Focus();
    }

    void Return()
    {
        eS.GetComponent<EventSystem>().SetSelectedGameObject(startButton, null);
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
