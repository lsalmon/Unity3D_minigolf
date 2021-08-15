using UnityEngine;

public class UISliderControl : MonoBehaviour
{
    // Manipulate canvas rotation for slider elements on the ball so that it doesnt swing around
    private Quaternion m_CanvasRotation;
    private float m_RotationSave;
    private Vector3 m_CanvasPosition;
    private Rigidbody m_Rigidbody;
    private Camera m_Camera;

    private void Start()
    {
        // Local rotation at init
        m_CanvasRotation = transform.parent.localRotation;
        m_CanvasPosition = transform.localPosition;
    }

    public void SetCamera(Camera camera)
    {
        m_Camera = camera;
    }

    public void SetRigidbody(Rigidbody body)
    {
        m_Rigidbody = body;
    }

    private void Update()
    {
        if (m_Rigidbody.IsSleeping())
        {
            // Freeze rotation of parent (empty object)
            transform.parent.rotation = m_CanvasRotation;

            Quaternion cam_rot = m_Camera.transform.rotation;
            cam_rot = Quaternion.Inverse(cam_rot);

            // Get angle between camera and ball
            float angle_diff = (transform.rotation.eulerAngles.y - cam_rot.eulerAngles.y);

            if (angle_diff < 5.0f && angle_diff > -5.0f)
            {
                Debug.Log("Aligned");
                m_RotationSave = 0.0f;
            }
            else
            {
                if (m_RotationSave == 0.0f)
                {
                    m_RotationSave = angle_diff;
                }
            }

            // Rotate slider around ball
            transform.RotateAround(transform.parent.position, Vector3.up, m_RotationSave);
        }
    }
}
