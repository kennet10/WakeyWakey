using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // An import that allows you to create Text objects.
using Ink.Runtime; // An external import that allows you to read Ink files.
using TMPro; // An external import that allows you to create TMP_Text objects.

//[CreateAssetMenu(fileName = "NewScriptReader", menuName = "Data/New Script Reader")]
[System.Serializable]
public class ScriptReader : MonoBehaviour
{
    public ScriptReader currentScene;
    private State state = State.COMPLETED; // Making sure that the text do not appear right away when entering a section
    // TODO: Make sure that the text automatically appear when user first start the game, instead of waiting on a Space input.

    [SerializeField]
    private TextAsset _InkJsonFile;
    private Story _StoryScript;

    public TMP_Text dialogueBox;
    public TMP_Text nameTag;

    private enum State
    {
        PLAYING, COMPLETED // These states are indicators if the current text is still in middle of printing on screen.
    }

    // A method that is called when the program first loads.
    void Start()
    {
        LoadStory();
    }

    // A method that is continuously called throughout program run. 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DisplayNextLine());
        }
    }

    // Method that loads provided Ink file (put in Unity Editor).
    // Also provides an External Function to InkScript allowing it to add names before the script text.
    void LoadStory()
    {
        _StoryScript = new Story(_InkJsonFile.text);
        _StoryScript.BindExternalFunction("Name", (string charName) => ChangeName(charName));
    }

    // Method that reads the content of the provided Ink file, and prints it on the screen.
    // TODO: Make sure that the text do not jumble if spamming Space.
    public IEnumerator DisplayNextLine()
    {
        if (_StoryScript.canContinue) // Checking if there is content to go through
        {
            dialogueBox.text = "";
            string text = _StoryScript.Continue(); // Gets next line
            text = text?.Trim(); // Removes white space from text
            state = State.PLAYING;
            int wordIndex = 0;
            while (state != State.COMPLETED)
            {
                dialogueBox.text += text[wordIndex]; // Displays new text one char by one char
                yield return new WaitForSeconds(0.05f);
                if (++wordIndex == text.Length)
                {
                    state = State.COMPLETED;
                }
            }
        }
        else
        {
            // TODO: Should initiate scene change instead of this.
            dialogueBox.text = "Please press the Skip button on top right to continue.";
        }
    }

    // Method that utilizes the External Method in InkScript to provide character name.
    public void ChangeName(string name)
    {
        string SpeakerName = name;

        nameTag.text = SpeakerName;
        //nameTag.color = 
        // TODO: Feel free to change the color of the protagonist text if you want, optional.
    }
}