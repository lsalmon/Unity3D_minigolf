using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    // Display text on screen
    private Text m_Text;
    private Coroutine boundsErr;
    private Coroutine strokeMsg;

    private void Start()
    {
        // Get canvas text object
        m_Text = GetComponentInChildren<Text>();

        // Move text upwards
        RectTransform textTransform = m_Text.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector2(150f, 40f);

        // Change font size
        m_Text.fontSize = 35;
        textTransform.sizeDelta = new Vector2(m_Text.fontSize * 10, 100f);
    }

    private IEnumerator PrintStrokes(uint strokes)
    {
        m_Text.text = "+"+strokes;
        yield return new WaitForSeconds(3);
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
        // If stroke message is displayed, wait 0.5s before stopping it
        if (strokeMsg != null)
        {
            yield return new WaitForSeconds(0.5f);
        }

        // Stop the rest of the coroutines
        StopAllCoroutines();

        // Move text to the left for message then reset it after
        RectTransform textTransform = m_Text.GetComponent<RectTransform>();
        Vector2 originalPosition = textTransform.anchoredPosition;
        textTransform.anchoredPosition = new Vector2(100f, 30f);

        m_Text.text = "Ball out of bounds, resetting position";
        yield return new WaitForSeconds(2);
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

        // Move text to the left
        RectTransform textTransform = m_Text.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector2(100f, 40f);
        m_Text.text = "Your score: +"+strokes;
    }
}
