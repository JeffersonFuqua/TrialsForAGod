using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class GalleryScript : MonoBehaviour
{
    public UIDocument addUI;
    private VisualElement rootElement;
    private Button bBack, bDisplay;
    private VisualElement vDisplayT, vDisplayW;
    public GalleryImages gPics;
    private List<Button> bImages = new List<Button>();
    public GameObject mainMenu, start;
    public EventSystem ES;
    private int x = 0;
    public int picCount;
    public void OnEnable()
    {
        rootElement = addUI.rootVisualElement;
        bDisplay = rootElement.Q<Button>("Display");
        bBack = rootElement.Q<Button>("Back");
        vDisplayT = rootElement.Q<VisualElement>("DisplayT");
        vDisplayW = rootElement.Q<VisualElement>("DisplayW");
        for(int i = 0; i < picCount; i++)
        {
            buttonMaker(x.ToString(), x);
            x++;
        }

        Startup();
        rootElement.Q<Button>(0.ToString()).Focus();        
    }

    public void buttonMaker(string name, int a)
    {
        Button newButton;
        newButton = rootElement.Q<Button>(name);
        newButton.clicked += () => Enlarge(a);
    }

    public void unAssign(Button button, int num)
    {
        button.clicked -= () => Enlarge(num);
    }

    public void Startup()
    {
        bDisplay.clicked += () => HideLarge();
        bBack.clicked += () => Return();
    }
    public void Return()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
        ES.GetComponent<EventSystem>().SetSelectedGameObject(start, null);
    }
    public void Enlarge(int image)
    { 
        for(int i = 0; i < picCount; i++)
        {
            rootElement.Q<Button>(i.ToString()).style.backgroundImage = null;
        }
        if(image >= 13)
        {
            vDisplayW.style.backgroundImage = gPics.imageList[image];
            vDisplayW.SetEnabled(true);
        }
        else
        {
            vDisplayT.style.backgroundImage = gPics.imageList[image];
            vDisplayT.SetEnabled(true);
        }
        bDisplay.BringToFront();
    }
    public void HideLarge()
    {
        vDisplayT.style.backgroundImage = null;
        vDisplayW.style.backgroundImage = null;
        vDisplayT.style.backgroundColor = Color.clear;
        vDisplayW.style.backgroundColor = Color.clear;
        for (int i = 0; i < picCount; i++)
        {
            rootElement.Q<Button>(i.ToString()).style.backgroundImage = gPics.imageList[i];
        }
        vDisplayT.SendToBack();
        vDisplayW.SendToBack();
        bDisplay.SendToBack();
        vDisplayT.SetEnabled(false);
        vDisplayW.SetEnabled(false);
        rootElement.Q<Button>(0.ToString()).Focus();
    }
    public void OnDisable()
    {
        bDisplay.clicked -= () => HideLarge();
    }
}
