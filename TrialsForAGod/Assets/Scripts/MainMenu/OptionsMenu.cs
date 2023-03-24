using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionsMenu : MonoBehaviour
{
    public OptionsMaster optionsVal;
    public GameObject mainMenu;
    public UIDocument uiDoc;
    private VisualElement rootElement;

    private static Slider masterVolSlider;
    private static Slider musicVolSlider;
    private static Slider sFXVolSlider;

    private Button back;


    private void OnEnable()
    {
        rootElement = uiDoc.rootVisualElement;
        masterVolSlider = rootElement.Q<Slider>("masterVolumeSlider");
        musicVolSlider = rootElement.Q<Slider>("musicVolumeSlider");
        sFXVolSlider = rootElement.Q<Slider>("sFXVolumeSlider");

        back = rootElement.Q<Button>("return");
        back.clicked += () => Return();

        masterVolSlider.value = optionsVal.masterVol;
        musicVolSlider.value = optionsVal.musicVol;
        sFXVolSlider.value = optionsVal.sFXVol;
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
    }

    void Return()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
