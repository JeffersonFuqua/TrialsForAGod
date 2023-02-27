using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NameInput : MonoBehaviour
{
    public UIDocument uiDoc;
    private VisualElement rootElement;
    public List<Button> alphaNumericButtons = new List<Button>();
    private TextField typedName;
    private Label playerName;
    private Label playerSign;
    private Button backspace;
    private Button space;
    private Button finish;

    private void OnEnable()
    {
        rootElement = uiDoc.rootVisualElement;
        typedName = rootElement.Q<TextField>("typeSpace");
        playerName = rootElement.Q<Label>("NameInput");
        playerSign = rootElement.Q<Label>("signature");
        backspace = rootElement.Q<Button>("backspace");
        space = rootElement.Q<Button>("space");
        finish = rootElement.Q<Button>("finish");

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
    }
    private void OnDisable()
    {
        backspace.clicked -= () => DeleteCharacter();
        space.clicked -= () => AddSpace();
        finish.clicked -= () => Finish();
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
        Debug.Log("finish");
    }

}
