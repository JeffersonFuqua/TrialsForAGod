using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GalleryScript : MonoBehaviour
{
    public UIDocument addUI;
    private VisualElement rootElement;
    private Button tester;

    public void OnEnable()
    {
        rootElement = addUI.rootVisualElement;
        tester = rootElement.Q<Button>("AresAngry");
        Debug.Log("Enabled");
        Enlarge();
    }

    public void Enlarge()
    {
        Debug.Log("Enlarge");
        tester.clicked += () => log();
    }
    public void log()
    {
        Debug.Log("clicked");
    }
}
