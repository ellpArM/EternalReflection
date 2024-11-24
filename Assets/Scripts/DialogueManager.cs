using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText; // Reference to the TMP text element.
    public GameObject backgroundPanel;  // Panel to serve as the black background.
    public GameObject win;  // Panel to serve as the black background.
    public GameObject loss;  // Panel to serve as the black background.
    public Image pixelArtRenderer; // Renderer to display pixel art.

    [Header("Dialogue Settings")]
    [TextArea(3, 5)]
    public List<string> dialogueLines; // List to hold dialogue lines.
    public List<Sprite> pixelArtSprites; // List of pixel art sprites matching dialogue lines.
    public float typingSpeed = 0.05f; // Speed at which characters are typed.

    private int currentLineIndex = 0; // Track the current line of dialogue.
    private bool isTyping = false; // Check if the text is currently being typed.
    public bool isStarter = false; // Check if the text is currently being typed.
    public bool losing = false; // Check if the text is currently being typed.

    [SerializeField] GameObject p;
    [SerializeField] GameObject e;
    [SerializeField] GameObject fade;

    void Start()
    {
        // Ensure the background starts as black.
        backgroundPanel.SetActive(true);
        pixelArtRenderer.gameObject.SetActive(true);

        // Start dialogue if there are lines available.
        if (dialogueLines.Count > 0)
        {
            ShowLine(currentLineIndex);
        }
    }

    void Update()
    {
        // Advance to the next line on key press.
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            NextLine();
        }
        if(isStarter == true)
        {
            if(p.GetComponent<PlayerHealth>().health == 0)
            {
                StartCoroutine(wet(loss));
            }
            else if(e.GetComponent<EnemyHealth>().health == 0)
            {
                StartCoroutine(wet(win));
            }
            
        }
    }

    private void ShowLine(int index)
    {
        // Update pixel art if sprites are provided.
        if (pixelArtSprites.Count > index && pixelArtSprites[index] != null)
        {
            pixelArtRenderer.sprite = pixelArtSprites[index];
        }

        // Start typing the line.
        StartCoroutine(TypeLine(dialogueLines[index]));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = ""; // Clear the text field.

        // Display the line character by character.
        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false; // Typing is done.
    }

    private void NextLine()
    {
        // Check if there are more lines.
        if (currentLineIndex < dialogueLines.Count - 1)
        {
            currentLineIndex++;
            ShowLine(currentLineIndex);
        }
        else
        {
            // End of dialogue, clear text and hide visuals.
            dialogueText.text = "";
            pixelArtRenderer.sprite = null;
            backgroundPanel.SetActive(false);
            pixelArtRenderer.gameObject.SetActive(false);
            Debug.Log("End of dialogue.");
            if(isStarter == false)
            {
                if (losing == false)
                { StartCoroutine(ender()); }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else
            {
                p.SetActive(true);
                e.SetActive(true);
            }
        }
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator wet(GameObject thing)
    {
        yield return new WaitForSeconds(2.5f);
        Instantiate(thing, this.transform.parent.transform);
        Destroy(gameObject);
    }
    IEnumerator ender()
    {
        Instantiate(fade, this.transform.parent.transform);
        yield return new WaitForSeconds(1.5f);
        
        NextScene();
        Destroy(gameObject);
    }
}
