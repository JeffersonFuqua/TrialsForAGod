using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GalleryScript : MonoBehaviour
{
    public UIDocument addUI;
    private VisualElement rootElement;
    private Button bDisplay;
    public GalleryImages gPics;
    private List<Button> bImages = new List<Button>();
    private int x = 0;
    public int picCount;
    /*
     * Make new scriptable object to hold all images X
     * See speakervalues ares pics for how to reference it
     * make multiple buttons and assign them a value for the reference in the all image list
     * probably should use a foreach loop if possible but I don't think it is 
     */
    public void OnEnable()
    {
        rootElement = addUI.rootVisualElement;
        bDisplay = rootElement.Q<Button>("Display");

        for(int i = 0; i < picCount; i++)
        {
            buttonMaker(x.ToString(), x);
            x++;
        }

        Startup();
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
        Debug.Log("Started");
        bDisplay.clicked += () => HideLarge();
    }
    public void Enlarge(int image)
    { 
        //fix this to use not just gPics.imageList[0]
        bDisplay.style.backgroundImage = gPics.imageList[image];
        bDisplay.BringToFront();
        bDisplay.SetEnabled(true);
    }
    public void HideLarge()
    {
        Debug.Log("Hide");
        bDisplay.style.backgroundImage = null;
        bDisplay.style.backgroundColor = Color.clear;
        bDisplay.SendToBack();
        bDisplay.SetEnabled(false);
    }
    public void OnDisable()
    {
        bDisplay.clicked -= () => HideLarge();
    }
}
