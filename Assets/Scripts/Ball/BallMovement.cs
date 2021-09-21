using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    private Slider m_LoadingSlider;
    private GameObject m_Canvas;
    private Camera m_Camera;
    private Rigidbody m_Rigidbody;
    public float m_chargeTime = 2.0f;
    public float m_force = 10.0f;
    private float m_time;
    private float m_charge;
    private bool m_released;

    void Start()
    {
        m_Rigidbody = transform.GetComponent<Rigidbody>();
        m_Canvas = GetComponentInChildren<Canvas>().gameObject;
        m_Canvas.SetActive(true);
        m_LoadingSlider = m_Canvas.GetComponentInChildren<Slider>();
        m_LoadingSlider.value = 0.0f;
        m_released = false;
    }

    public void SetCamera(Camera camera)
    {
        m_Camera = camera;
    }

    void Update()
    {
        // Only fire if ball is not moving
        if (m_Rigidbody.IsSleeping())
        {
            // First mouse click press
            if (Input.GetButtonDown("Fire1")) 
            {
                m_charge = 0.0f;
                m_released = false;

                // Get starting time
                m_time = Time.time;
            }
            // Mouse click still pressed
            else if (Input.GetButton("Fire1") && !m_released)
            {
                // Get time elapsed between press and release of left mouse click
                m_charge = (Time.time - m_time) % m_chargeTime;
                // Display slider growing in a loop with return to 0
                // TODO: fix animation and slider size
                m_LoadingSlider.value = (m_charge / m_chargeTime);
            }
            // Mouse click released
            else if (Input.GetButtonUp("Fire1") && !m_released)
            {
                m_released = true;

                // Apply force to move ball
                Vector3 push = m_Camera.transform.forward.normalized;
                push.y = 0.0f;
                m_Rigidbody.AddForce (push * (m_charge / m_chargeTime) * m_force, ForceMode.Impulse);
            }
        }
        else
        {
            m_LoadingSlider.value = 0.0f;
        }
    }
}
