using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GalleryScript : MonoBehaviour
{
    public UIDocument addUI;
    private VisualElement rootElement;
    private Button tester;
    private Button testerTwo;
    public GalleryImages gPics;
    private int x = 0;
    /*
     * Make new scriptable object to hold all images X
     * See speakervalues ares pics for how to reference it
     * make multiple buttons and assign them a value for the reference in the all image list
     * probably should use a foreach loop if possible but I don't think it is 
     */
    public void OnEnable()
    {
        rootElement = addUI.rootVisualElement;
        tester = rootElement.Q<Button>("AresAngry");
       // Debug.Log(tester.style.backgroundImage.value.sprite.name);
        testerTwo = rootElement.Q<Button>("Display");
        Debug.Log("Enabled");
        Startup();
    }

    public void Startup()
    {
        Debug.Log("Started");
        tester.clicked += () => Enlarge();
        testerTwo.clicked += () => HideLarge();
    }
    public void Enlarge()
    { 
        testerTwo.style.backgroundImage = gPics.imageList[0];
       // testerTwo.style.backgroundColor = Color.black;
        testerTwo.BringToFront();
        testerTwo.SetEnabled(true);
    }
    public void HideLarge()
    {
        Debug.Log("Hide");
        testerTwo.style.backgroundImage = null;
        testerTwo.style.backgroundColor = Color.clear;
        testerTwo.SendToBack();
        testerTwo.SetEnabled(false);
    }
    public void OnDisable()
    {
        tester.clicked -= () => Enlarge();
        testerTwo.clicked -= () => HideLarge();
    }
}
