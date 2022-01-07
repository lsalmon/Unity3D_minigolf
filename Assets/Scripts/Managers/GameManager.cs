using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject m_BallPrefab;
    public Camera m_Camera;
    public BallManager m_Ball;
    public Collider m_HoleCollider;
    public Canvas m_Display;
    public Vector3 m_CamOffset   = new Vector3(5f, 3f, 0.0f);
    private bool started = false;
    private uint running = 0;
    private CameraControl m_CameraControl;
    private UIDisplay uidisplay;

    void Start()
    {
        m_Camera.transform.position = m_CamOffset;
        m_CameraControl = m_Camera.GetComponent<CameraControl>();
        // Disable camera control to not interfere with the starting screen
        m_CameraControl.enabled = false;

        // Get UI component to display text on screen
        uidisplay = m_Display.GetComponent<UIDisplay>();
    }

    // Display starting screen with rotating camera
    void Update()
    {
        if (!started)
        {
            if (Input.anyKey)
            {
                started = true;
                SpawnBall();
            }
            else
            {
                // Rotate camera around starting position of the ball
                m_Camera.transform.RotateAround(m_Ball.m_StartingPosition.position, Vector3.up, 20 * Time.deltaTime);
                m_Camera.transform.LookAt(m_Ball.m_StartingPosition);
            }
        }
        else
        {
            // Pause menu
            if (Input.GetButtonDown("Submit"))
            {
                running = 1 - running;
                Time.timeScale = running;
                if (running == 0)
                {
                    uidisplay.DisplayPauseMenu();
                }
                else
                {
                    uidisplay.Resume();
                }
            }
        }
    }

    // Spawn the ball
    void SpawnBall()
    {
        // Clean starting screen
        uidisplay.CleanDisplay();

        // Enable camera control for the ball
        m_CameraControl.enabled = true;

        m_Ball.m_Instance = Instantiate(m_BallPrefab, m_Ball.m_StartingPosition.position, m_Ball.m_StartingPosition.rotation) as GameObject;
        m_Ball.Setup(m_Camera, this, m_HoleCollider);
        m_CameraControl.SetPlayer(m_Ball.m_Instance);
    }

    public void CountStrokes(uint strokes)
    {
        uidisplay.DisplayStrokes(strokes);
    }

    public void OutOfBounds()
    {
        uidisplay.DisplayOutOfBounds();
    }

    // Stop the game if ball is in hole
    public void End(uint strokes)
    {
        uidisplay.DisplayEnd(strokes);
        Debug.Log("End of game, strokes "+strokes);
    }
}
