using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class GalleryScript : MonoBehaviour
{
    public UIDocument addUI;
    private VisualElement rootElement;
    private Button bDisplay, bBack;
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
       // bDisplay.style.backgroundImage = gPics.imageList[image];
        for(int i = 0; i < picCount; i++)
        {
            rootElement.Q<Button>(i.ToString()).style.backgroundImage = null;
        }
        bDisplay.BringToFront();
        bDisplay.SetEnabled(true);
    }
    public void HideLarge()
    {
        bDisplay.style.backgroundImage = null;
        bDisplay.style.backgroundColor = Color.clear;
        for (int i = 0; i < picCount; i++)
        {
           // rootElement.Q<Button>(i.ToString()).style.backgroundImage = gPics.imageList[i];
        }
        bDisplay.SendToBack();
        bDisplay.SetEnabled(false);
        rootElement.Q<Button>(0.ToString()).Focus();
    }
    public void OnDisable()
    {
        bDisplay.clicked -= () => HideLarge();
    }
}
