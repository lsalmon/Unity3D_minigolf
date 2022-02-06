using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    // Display text on screen
    public GameManager gameManager;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    private Text m_Text;
    private Coroutine boundsErr;
    private Coroutine strokeMsg;

    private void Start()
    {
        // Find and assign menu objects,
        // which are children of the gameobject the script is attached to
        mainMenu = transform.Find("MainMenu").gameObject;

        if (mainMenu == null)
        {
            Debug.Log("Could not retrieve main menu object");
        }

        pauseMenu = transform.Find("OptionMenu").gameObject;

        if (pauseMenu == null)
        {
            Debug.Log("Could not retrieve pause menu object");
        }

        // Disable the pause menu 
        pauseMenu.SetActive(false);

        // Enable the main menu
        mainMenu.SetActive(true);

        // Init of information display canvas
        // (directly linked to the gameobject the script is attached to)
        // Get canvas text object and align it on the center
        m_Text = GetComponentInChildren<Text>();
        m_Text.alignment = TextAnchor.MiddleCenter;

        // Move text upwards
        RectTransform textTransform = m_Text.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector2(0f, 50f);

        // Change font size
        m_Text.fontSize = 50;
        textTransform.sizeDelta = new Vector2(m_Text.fontSize * 15, 100f);
 
        // TODO: Find out why the main menu is not clickable at init
        StartGame();
    }

    // Text display functions
    private IEnumerator PrintStrokes(uint strokes)
    {
        m_Text.text = "+"+strokes;
        yield return new WaitForSeconds(2);
        m_Text.text = "";

        strokeMsg = null;
    }

    public void StartGame()
    {
        // Disable the main menu
        mainMenu.SetActive(false);

        // Display starting text (information display canvas)
        m_Text.text = "Press any key to start";

        // Tell the manager to start the game
        gameManager.StartGame();
    }

    public void DisplayStrokes(uint strokes)
    {
        // Stop all the coroutines
        StopAllCoroutines();
        strokeMsg = StartCoroutine(PrintStrokes(strokes));
    }

    private IEnumerator PrintOutOfBounds()
    {
        // If stroke message is displayed, wait 0.3s before stopping it
        if (strokeMsg != null)
        {
            yield return new WaitForSeconds(0.3f);
        }

        // Stop stroke message
        StopCoroutine(strokeMsg);

        // Move text higher for message then reset it after
        RectTransform textTransform = m_Text.GetComponent<RectTransform>();
        Vector2 originalPosition = textTransform.anchoredPosition;
        textTransform.anchoredPosition = new Vector2(0f, 75f);

        m_Text.text = "Ball out of bounds, resetting position";
        yield return new WaitForSeconds(1);
        m_Text.text = "";

        textTransform.anchoredPosition = originalPosition;
        boundsErr = null;
    }

    public void DisplayOutOfBounds()
    {
        boundsErr = StartCoroutine(PrintOutOfBounds());
    }

    public void DisplayEnd(uint strokes)
    {
        // Stop all the coroutines
        StopAllCoroutines();

        m_Text.text = "Your score: +"+strokes;
    }

    public void CleanDisplay()
    {
        // Reset font size
        m_Text.fontSize = 35;
        RectTransform textTransform = m_Text.GetComponent<RectTransform>();
        textTransform.sizeDelta = new Vector2(m_Text.fontSize * 10, 100f);

        // Reset text
        m_Text.text = "";
    }

    // Menu functions
    public void DisplayPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    // Resume function called when pressing pause key
    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

    // Resume function called when clicking on the Resume button
    public void ResumeFromMenu()
    {
        pauseMenu.SetActive(false);

        // Tell the manager to resume game
        gameManager.ResumeFromMenu();
    }
}
