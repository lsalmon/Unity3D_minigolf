using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    // Display text on screen
    private Text m_Text;

    private void Start()
    {
        // Get canvas text object
        m_Text = GetComponentInChildren<Text>();

        // Move text upwards
        RectTransform texttransform = m_Text.GetComponent<RectTransform>();
        texttransform.anchoredPosition = new Vector2(150f, 40f);

        // Change font size
        m_Text.fontSize = 35;
        texttransform.sizeDelta = new Vector2(m_Text.fontSize * 10, 100f);
    }

    private IEnumerator PrintStrokes(uint strokes)
    {
        m_Text.text = "+"+strokes;
        yield return new WaitForSeconds(3);
        m_Text.text = "";
    }

    public void DisplayStrokes(uint strokes)
    {
        StopAllCoroutines();
        StartCoroutine(PrintStrokes(strokes));
    }

    public void DisplayEnd(uint strokes)
    {
        // Move text to the left
        RectTransform texttransform = m_Text.GetComponent<RectTransform>();
        texttransform.anchoredPosition = new Vector2(100f, 40f);
        m_Text.text = "Your score: +"+strokes;
    }
}
