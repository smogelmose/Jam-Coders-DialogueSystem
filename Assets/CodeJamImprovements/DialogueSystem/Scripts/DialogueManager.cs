using System.Collections;
using TMPro;
using UnityEngine;

namespace CodeJamImprovements.DialogueSystem.Scripts
{
    public class DialogueManager : MonoBehaviour
    {
        // The TextMeshProUGUI component used to display the dialogue text
        public TextMeshProUGUI textComponent;
        // The lines of dialogue to display
        public string[] lines;
        // The speed at which to display the text (in seconds per character)
        public float textSpeed;
        // The current index of the line being displayed
        private int _index;

        // Start is called before the first frame update
        void Start()
        {
            // Clear the text component
            textComponent.text = string.Empty;
            // Start the dialogue
            StartDialogue();
        }

        void Update()
        {
            // If the entire line has been displayed
            if (textComponent.text == lines[_index])
            {
                // If the left mouse button is pressed, move to the next line
                if (Input.GetMouseButtonDown(0))
                {
                    NextLine();
                }
            }
            // If the entire line has not been displayed
            else
            {
                // If the left mouse button is pressed, skip to the end of the line
                if (Input.GetMouseButtonDown(0))
                {
                    StopAllCoroutines(); // Stop typing the line
                    textComponent.text = lines[_index];
                }
            }
        }

        void StartDialogue()
        {
            // Reset the line index to the first line
            _index = 0;
            // Begin typing the first line
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            // Type out the line one character at a time
            foreach (char c in lines[_index])
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed); // Wait for the specified amount of time before typing the next character
            }
        }

        void NextLine()
        {
            // If there are more lines to display
            if (_index < lines.Length - 1)
            {
                // Move to the next line
                _index++;
                // Clear the text component
                textComponent.text = string.Empty;
                // Begin typing the next line
                StartCoroutine(TypeLine());
            }
            // If there are no more lines to display
            else
            {
                // Deactivate the dialogue box
                gameObject.SetActive(false);
            }
        }

        void Awake()
        {
            // Load the dialogue lines from a text asset called "Bear"
            var textAsset = Resources.Load<TextAsset>("Bear");
            // If the text asset cannot be loaded, log an error and return
            if (textAsset == null)
            {
                Debug.LogError("Failed to load dialogue from resources.");
                return;
            }
            // Split the text asset into lines based on the newline character
            lines = textAsset.text.Split('\n');
            // Log that the text was loaded successfully
            Debug.Log("Text loaded successfully: " + string.Join("\n", lines)); // Join the lines back together with a newline character between each line
        }
    }
}

// Inspired by https://www.youtube.com/watch?v=8oTYabhj248&list=PLCDqd_BDUOne2-xb6PtOGhhNA8Qoh1G4V&index=7