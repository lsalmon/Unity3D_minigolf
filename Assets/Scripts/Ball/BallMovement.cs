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
    private float m_time;
    private float m_force = 10.0f;

    void Start()
    {
        m_Rigidbody = transform.GetComponent<Rigidbody>();
        m_Canvas = GetComponentInChildren<Canvas>().gameObject;
        m_Canvas.SetActive(true);
        m_LoadingSlider = m_Canvas.GetComponentInChildren<Slider>();
        m_LoadingSlider.value = 0.1f;
    }

    public void SetCamera(Camera camera)
    {
        m_Camera = camera;
    }

    private IEnumerator Firing()
    {
        // TODO: Draw charging animation on ball
        m_LoadingSlider.value = 0.8f;
        yield return null;
    }

    void Update()
    {
        // Only fire if ball is not moving
        if (m_Rigidbody.IsSleeping())
        {
            if (Input.GetButtonDown("Fire1")) 
            {
                StopAllCoroutines();
                StartCoroutine(Firing());

                // Get starting time
                m_time = Time.time;
            }

            if (Input.GetButtonUp("Fire1")) 
            {
                StopAllCoroutines();
                m_LoadingSlider.value = 0.1f;

                // Get time elapsed between press and release of left mouse click
                float charge = Time.time - m_time;
                if (charge > 1.0f)
                {
                    charge = 1.0f;
                }

                // Apply force
                Vector3 push = m_Camera.transform.forward.normalized;
                push.y = 0.0f;
                m_Rigidbody.AddForce (push * charge * m_force, ForceMode.Impulse);
            }
        }
        else
        {
            m_LoadingSlider.value = 0.0f;
        }
    }
}
