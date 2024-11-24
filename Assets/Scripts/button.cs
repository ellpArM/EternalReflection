using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SceneButton : MonoBehaviour
{
    public Button button;                // Reference to the button component
    public TMP_Text txt;                // Reference to the button component
    public Color normalColor = Color.white; // Default button color
    public Color hoverColor = Color.green;  // Color when hovered over
    public float highlightScale = 1.1f;   // Scale when hovered over
    private Vector3 originalScale;       // Original scale of the button

    void Start()
    {
        // Ensure the button component is assigned
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        originalScale = button.transform.localScale;  // Store the button's original scale
    }

    // This method is called when the button is clicked
    public void LoadSceneByIndex()
    {
        // Load the scene by index (1 is the second scene)
        SceneManager.LoadScene(1); // Index 1 refers to the second scene in the build settings
    }

    public void quit()
    {
        Application.Quit();
    }


}
