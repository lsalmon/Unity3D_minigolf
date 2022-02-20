using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    private Slider m_LoadingSlider;
    private GameObject m_Canvas;
    private Camera m_Camera;
    private GameObject m_Hole;
    private Collider[] m_Colliders;
    private GameManager m_Manager;
    private Rigidbody m_Rigidbody;
    private PlaySounds m_PlaySounds;
    private float m_ChargeTime = 2.0f;
    private float m_Force = 10.0f;
    private float time;
    private float charge;
    private bool released = false;
    private bool reset = false;
    private uint strokes = 0;
    private Vector3 previousPosition;
    private bool move = true;

    void Start()
    {
        m_Rigidbody = transform.GetComponent<Rigidbody>();
        previousPosition = transform.position;
        m_Canvas = GetComponentInChildren<Canvas>().gameObject;
        m_Canvas.SetActive(true);
        m_LoadingSlider = m_Canvas.GetComponentInChildren<Slider>();
        m_LoadingSlider.value = 0.0f;
        m_PlaySounds = transform.GetComponent<PlaySounds>();
    }

    public void SetCamera(Camera camera)
    {
        m_Camera = camera;
    }

    public void SetPower(float chargeTime, float force)
    {
        m_ChargeTime = chargeTime;
        m_Force = force;
    }

    public void SetHole(GameObject hole)
    {
        m_Hole = hole;
    }

    public void SetColliders(Collider[] colliders)
    {
        m_Colliders = colliders;
    }

    public void SetManager(GameManager manager)
    {
        m_Manager = manager;
    }

    public void EnableMovement(bool enabled)
    {
        move = enabled;
    }

    private void ResetBall()
    {
        transform.position = previousPosition;
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.Sleep();
        m_Manager.OutOfBounds();
    }

    void Update()
    {
        // Only fire if ball is not moving
        // and is allowed to move (not in pause menu)
        if (move && m_Rigidbody.IsSleeping())
        {
            reset = false;

            // First mouse click press
            if (Input.GetButtonDown("Fire1")) 
            {
                charge = 0.0f;
                released = false;

                // Get starting time
                time = Time.time;

                // Store position before firing
                previousPosition = transform.position;

                // Start playing loading sound
                m_PlaySounds.StartLoading();
            }
            // Mouse click still pressed
            else if (Input.GetButton("Fire1") && !released)
            {
                // Get time elapsed between press and release of left mouse click
                charge = (Time.time - time) % m_ChargeTime;
                float loadingValue = (charge / m_ChargeTime);

                // Display slider growing in a loop with return to 0
                m_LoadingSlider.value = loadingValue;

                // Change pitch of loading sound along with display slider
                m_PlaySounds.SetPitch(loadingValue);
            }
            // Mouse click released
            else if (Input.GetButtonUp("Fire1") && !released)
            {
                released = true;

                // Apply force to move ball
                Vector3 push = m_Camera.transform.forward.normalized;
                push.y = 0.0f;
                m_Rigidbody.AddForce (push * (charge / m_ChargeTime) * m_Force, ForceMode.Impulse);

                // Increment strokes
                strokes++;
                m_Manager.CountStrokes(strokes);

                // Stop playing loading sound
                m_PlaySounds.EndLoading();
            }
        }
        else
        {
            m_LoadingSlider.value = 0.0f;

            // Reset ball position and speed if out of bounds and not currently being reset
            if (!reset)
            {
                // Check if ground collider contains ball
                if (!m_Colliders[0].bounds.Contains(transform.position))
                {
                    Ray ray = new Ray(transform.position, Vector3.down);
                    RaycastHit hit;
                    reset = true;

                    foreach (Collider collider in m_Colliders)
                    {
                        // For all colliders (ground + top),
                        // cast ray right below the ball to check if the ball isnt simply in the air due to a bump
                        // If at least a collider is below the ball, abort reset
                        if (collider.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            reset = false;
                            break;
                        }
                    }

                    if (reset)
                    {
                        Invoke("ResetBall", 0.5f);
                    }
                }
            }
        }
    }

    // Check if ball is in hole
    void OnTriggerEnter(Collider trigger)
    {  
        if (GameObject.ReferenceEquals(trigger.gameObject, m_Hole))
        {
            // Send message to GameManager to display score and end the game
            m_Manager.End(strokes);
        }
    }
}
