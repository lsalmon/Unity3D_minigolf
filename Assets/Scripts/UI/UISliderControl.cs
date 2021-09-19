using UnityEngine;

public class UISliderControl : MonoBehaviour
{
    // Manipulate canvas rotation for slider elements on the ball so that it doesnt swing around
    private Quaternion m_CanvasRotation;
    private float m_RotationSave;
    private Rigidbody m_Rigidbody;
    private Camera m_Camera;

    private void Start()
    {
        // Local rotation at init
        m_CanvasRotation = transform.parent.localRotation;
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
        // Only move slider around when ball is not moving
        if (m_Rigidbody.IsSleeping())
        {
            // Freeze rotation of parent (empty object)
            transform.parent.rotation = m_CanvasRotation;

            // Get camera rotation
            Quaternion cam_rot = m_Camera.transform.rotation;

            // Invert camera rotation
            float angle = (cam_rot.eulerAngles.y + 180) % 360;

            // Get direction between camera and ball
            // then invert it and ignore Y component
            Vector3 position = m_Rigidbody.position;
            Vector3 pos_cam = m_Camera.transform.position;
            Vector3 dir_canvas = -(position - pos_cam).normalized;

            // Set position of slider to match the rotation
            // (have slider behind ball at all angles)
            position.x += dir_canvas.x * 0.4f;
            position.z += dir_canvas.z * 0.4f;
            transform.position = position;

            // Set rotation of slider
            Quaternion ball_rot = Quaternion.Euler(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);
            transform.rotation = ball_rot;
        }
    }
}
