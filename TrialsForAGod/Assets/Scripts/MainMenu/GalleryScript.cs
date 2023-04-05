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
    private int x = 0, h = 0;
    public int picCount;
    public void OnEnable()
    {
            rootElement = addUI.rootVisualElement;
            bDisplay = rootElement.Q<Button>("Display");
            bBack = rootElement.Q<Button>("return");
            vDisplayT = rootElement.Q<VisualElement>("DisplayT");
            vDisplayW = rootElement.Q<VisualElement>("DisplayW");

            for(int i = 0; i < picCount; i++)
            {
                buttonMaker(x.ToString(), x);
                x++;
            }
        x = 0;
        Startup();
        rootElement.Q<Button>("Display").RegisterCallback<NavigationMoveEvent>(e =>
        {
            switch (e.direction)
            {
                //case NavigationMoveEvent.Direction.Up: rootElement.Q<Button>((i-1).ToString()).Focus(); break;
                case NavigationMoveEvent.Direction.Down:
                    bBack.Focus();
                    break;
                case NavigationMoveEvent.Direction.Left:
                    break;
                case NavigationMoveEvent.Direction.Right:
                    break;
            }
            e.PreventDefault();
        });
                rootElement.Q<Button>(0.ToString()).Focus();        
    }

    public void buttonMaker(string name, int a)
    {
        Button newButton = new Button();
        newButton.AddToClassList("button");
        newButton.AddToClassList("button:hover");
        newButton.AddToClassList("button:focus");
        newButton = rootElement.Q<Button>(name);
        newButton.clicked += () => Enlarge(a);
        newButton.style.borderBottomColor = Color.clear;
        newButton.style.borderTopColor = Color.clear;
        newButton.style.borderLeftColor = Color.clear;
        newButton.style.borderRightColor = Color.clear;
    }

    public void unAssign(Button button, int num)
    {
        button.clicked -= () => Enlarge(num);
    }
    public void Startup()
    {
        bDisplay.clicked += () => HideLarge();
        bBack.clicked += () => Return();
        for(int i = 0; i < picCount; i++)
            rootElement.Q<Button>(i.ToString()).RegisterCallback<NavigationMoveEvent>(e =>
            {
                Debug.Log(h);
                if(h > 0 && h != picCount-1)
                {
                    switch (e.direction)
                    {
                        //case NavigationMoveEvent.Direction.Up: rootElement.Q<Button>((i-1).ToString()).Focus(); break;
                        case NavigationMoveEvent.Direction.Down: 
                            bBack.Focus(); 
                            break;
                        case NavigationMoveEvent.Direction.Left:
                            rootElement.Q<Button>((h - 1).ToString()).Focus();
                            h--;
                            break;
                        case NavigationMoveEvent.Direction.Right:
                            rootElement.Q<Button>((h + 1).ToString()).Focus();
                            h++;
                            break;
                    }
                }
                else if (h == 0)
                {
                    switch (e.direction)
                    {
                    case NavigationMoveEvent.Direction.Right: 
                            rootElement.Q<Button>((h + 1).ToString()).Focus();
                            h++;
                            break;
                    case NavigationMoveEvent.Direction.Left:
                            rootElement.Q<Button>((picCount-1).ToString()).Focus();
                            h = picCount-1;
                            break;
                    case NavigationMoveEvent.Direction.Down: 
                            bBack.Focus(); 
                            break;
                    }
                }
                else
                {
                    switch (e.direction)
                    {
                    case NavigationMoveEvent.Direction.Left:
                            rootElement.Q<Button>((h - 1).ToString()).Focus();
                            h--;
                            break;
                    case NavigationMoveEvent.Direction.Right:
                            rootElement.Q<Button>((0).ToString()).Focus();
                            h = 0;
                            break;
                    case NavigationMoveEvent.Direction.Down:
                           bBack.Focus();
                           break;
                    }
                }
            e.PreventDefault();
        });
    }
    public void Return()
    {
        mainMenu.SetActive(true);
        ES.GetComponent<EventSystem>().SetSelectedGameObject(start, null);
        gameObject.SetActive(false);
    }
    public void Enlarge(int image)
    { 
        for(int i = 0; i < picCount; i++)
        {
            rootElement.Q<Button>(i.ToString()).style.backgroundImage = null;
        }
        if(image >= 12)
        {
            vDisplayW.style.backgroundImage = gPics.imageList[image];
            vDisplayW.SetEnabled(true);
            vDisplayT.SetEnabled(false);
        }
        else
        {
            vDisplayT.style.backgroundImage = gPics.imageList[image];
            vDisplayT.SetEnabled(true);
            vDisplayW.SetEnabled(false);
        }
        bDisplay.BringToFront();
        rootElement.Q<Button>("Display").Focus();

    }
    public void HideLarge()
    {
        Debug.Log("hiding");
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
        for (int d = 0; d < picCount; d++)
        {
               unAssign(rootElement.Q<Button>(d.ToString()), d);
            //   rootElement.Q<Button>(d.ToString()).clicked -= () => Enlarge(d);
            rootElement.Q<Button>(d.ToString()).RemoveFromClassList("button");
        }
    }
}
