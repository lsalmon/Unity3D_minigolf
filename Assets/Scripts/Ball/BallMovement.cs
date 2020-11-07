using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Camera m_Camera;
    private Rigidbody m_Rigidbody;
    private float m_time;
    private float m_force = 10.0f;

    void Start()
    {
        m_Rigidbody = transform.GetComponent<Rigidbody>();
    }

    public void SetCamera(Camera camera)
    {
        m_Camera = camera;
    }


    private IEnumerator Firing()
    {
        // TODO: Draw charging animation on ball
        yield return null;
    }

    void Update()
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
}
