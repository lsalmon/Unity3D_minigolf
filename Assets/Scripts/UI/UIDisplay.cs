using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    // Display text on screen
    private Text m_Text;
    private Coroutine boundsErr;
    private Coroutine strokeMsg;
    public GameObject pauseMenu;

    private void Start()
    {
        // Disable the pause menu 
        pauseMenu.SetActive(false);

        // Get canvas text object and align it on the center
        m_Text = GetComponentInChildren<Text>();
        m_Text.alignment = TextAnchor.MiddleCenter;

        // Move text upwards
        RectTransform textTransform = m_Text.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector2(0f, 50f);

        // Change font size
        m_Text.fontSize = 50;
        textTransform.sizeDelta = new Vector2(m_Text.fontSize * 15, 100f);

        // Display starting text
        m_Text.text = "Press any key to start";
    }

    // Text display functions
    private IEnumerator PrintStrokes(uint strokes)
    {
        m_Text.text = "+"+strokes;
        yield return new WaitForSeconds(2);
        m_Text.text = "";

        strokeMsg = null;
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

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }
}
