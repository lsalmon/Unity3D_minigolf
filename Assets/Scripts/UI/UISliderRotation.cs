using UnityEngine;

public class UISliderRotation : MonoBehaviour
{
    // Manipulate canvas rotation for slider elements on the ball so that it doesnt roll around
    private Quaternion m_CanvasRotation;

    private void Start()
    {
        // Local rotation at init
        m_CanvasRotation = transform.localRotation;
    }

    public void SetRotation(Quaternion newRotation)
    {
        m_CanvasRotation = newRotation;
    }

    private void Update()
    {
        //transform.rotation = m_CanvasRotation;
        transform.localEulerAngles = new Vector3(-90, 0, 0);
    }
}
