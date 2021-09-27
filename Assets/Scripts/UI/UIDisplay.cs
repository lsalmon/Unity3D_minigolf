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
        texttransform.anchoredPosition = new Vector2(100f, 40f);

        // Change font size
        m_Text.fontSize = 35;
        texttransform.sizeDelta = new Vector2(m_Text.fontSize * 10, 100f);
    }

    public void DisplayStrokes(uint strokes)
    {
        m_Text.text = "Strokes: "+strokes;
    }

    public void DisplayEnd(uint strokes)
    {
        m_Text.text = "You Won in "+strokes+" strokes";
    }
}
