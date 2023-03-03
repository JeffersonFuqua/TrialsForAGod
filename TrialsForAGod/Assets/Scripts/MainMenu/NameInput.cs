using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class NameInput : MonoBehaviour
{
    public PlayerValues playerVal;
    public GameObject mainMenu;
    public UIDocument uiDoc;
    public GameObject fadeOut;
    private VisualElement rootElement;
    public List<Button> alphaNumericButtons = new List<Button>();
    private TextField typedName;
    private Label playerName;
    private Label playerSign;
    private Button backspace;
    private Button space;
    private Button finish;
    private Label noNameWarning;
    private Button back;

    private void OnEnable()
    {
        rootElement = uiDoc.rootVisualElement;
        typedName = rootElement.Q<TextField>("typeSpace");
        playerName = rootElement.Q<Label>("NameInput");
        playerSign = rootElement.Q<Label>("signature");
        backspace = rootElement.Q<Button>("backspace");
        space = rootElement.Q<Button>("space");
        finish = rootElement.Q<Button>("finish");
        noNameWarning = rootElement.Q<Label>("noName");
        back = rootElement.Q<Button>("return");

        char capitalAlphabet = 'A';
        for(int i = 0; i < 26; i++)
        {
            AssignButton(capitalAlphabet.ToString());
            capitalAlphabet = (char)((int)(capitalAlphabet) + 1);
        }

        char lowerAlphabet = 'a';
        for (int i = 0; i < 26; i++)
        {
            AssignButton(lowerAlphabet.ToString());
            lowerAlphabet = (char)((int)(lowerAlphabet) + 1);
        }

        backspace.clicked += () => DeleteCharacter();
        space.clicked += () => AddSpace();
        finish.clicked += () => Finish();
        back.clicked += () => Return();
    }
    private void OnDisable()
    {
        backspace.clicked -= () => DeleteCharacter();
        space.clicked -= () => AddSpace();
        finish.clicked -= () => Finish();
        back.clicked -= () => Return();
    }

    private void Update()
    {
        //allows fake keyboard to type unless real keyboard does
        if(typedName.text != "" && typedName.text.Length < 8)
        {
            playerName.text = typedName.text;
            playerSign.text = playerName.text;
        }
    }

    private void AssignButton(string characterName)
    {
        Button button;
        button = rootElement.Q<Button>(characterName);
        if(button != null)
        {
            button.clicked += () => AddCharacter(button.name);
            //button.
        }
    }

    private void RemoveAssignment(Button button)
    {
        button.clicked -= () => AddCharacter(button.name);
        backspace.clicked -= () => DeleteCharacter();
        space.clicked -= () => AddSpace();
    }

    private void AddCharacter(string value)
    {
        if(playerName.text.Length < 8)
        {
            playerName.text += value;
        }
        playerSign.text = playerName.text;
    }
    private void DeleteCharacter()
    {
        // deletes all for some reason
        if(playerName.text.Length != 0)
        {
            playerName.text = playerName.text.Remove(playerName.text.Length - 1);
        }
        playerSign.text = playerName.text;
    }

    private void AddSpace()
    {
        string spaceChar = " ";
        if (playerName.text.Length < 8)
        {
            playerName.text += spaceChar;
        }
        playerSign.text = playerName.text;
    }

    private void Finish()
    {
        if(playerName.text != "")
        {
            finish.clicked -= () => Finish();
            finish.visible = false;
            fadeOut.GetComponent<Animator>().speed = 0.5f;
            fadeOut.GetComponent<Animator>().SetTrigger("fadeOut");

            playerVal.playerName = playerSign.text;
            StartCoroutine(loadNextScene());
        }
        else
        {
            noNameWarning.visible = true;
        }
    }
    IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }

    private void Return()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}