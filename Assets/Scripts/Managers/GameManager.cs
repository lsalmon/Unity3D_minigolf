using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Camera variables
    public Camera m_Camera;
    public Vector3 m_CamOffset = new Vector3(5f, 3f, 0.0f);
    private CameraControl cameraControl;

    // Ball variables
    public GameObject m_BallPrefab;
    // Colliders to detect when the ball goes out of the course
    // (ground collider must be first in array)
    public Collider[] m_OutOfBoundsColliders;
    // Starting position for the ball
    public Transform m_StartingPosition;
    // Used to detect when the ball is in the hole
    // (meaning the player won)
    public GameObject m_Hole;
    // Charging time and force applied when hitting the ball
    // (max 2s)
    public float m_ChargeTime = 2.0f;
    public float m_Force = 10.0f;
    // Slider used when hitting the ball
    private UISliderControl uiSlider;
    // Instance of the ball
    private GameObject ballInstance;
    // Script controlling the ball
    private BallMovement ballMovement;

    // Display variables (menus)
    public Canvas m_Display;
    // "any key" screen control
    private bool started = false;
    // Pause menu control
    private uint unpaused = 1;
    private UIDisplay uiDisplay;

    // Audio variables
    public AudioClip m_AudioBackground;
    public AudioClip m_AudioCompleted;
    private AudioSource audioSource;

    void Start()
    {
        m_Camera.transform.position = m_CamOffset;
        cameraControl = m_Camera.GetComponent<CameraControl>();
        // Disable camera control to not interfere with the "any key" screen
        cameraControl.enabled = false;

        // Get UI component to display text on screen
        uiDisplay = m_Display.GetComponent<UIDisplay>();

        // Get audio source for background music and winning sound
        audioSource = GetComponent<AudioSource>();
    }

    // Display "any key" screen with rotating camera
    void Update()
    {
        // If user hasnt pressed on any key, show golf hole
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
                m_Camera.transform.RotateAround(m_StartingPosition.position, Vector3.up, 20 * Time.deltaTime);
                m_Camera.transform.LookAt(m_StartingPosition);
            }
        }
        else
        {
            // Pause menu
            if (Input.GetButtonDown("Submit"))
            {
                unpaused = 1 - unpaused;
                Time.timeScale = unpaused;
                if (unpaused == 0)
                {
                    // Pause background music
                    audioSource.Pause();
                    // Disable control on the ball
                    EnableBall(false);
                    uiDisplay.DisplayPauseMenu();
                }
                else
                {
                    uiDisplay.Resume();
                    // Reenable control on the ball
                    EnableBall(true);
                    // Resume background music
                    audioSource.Play();
                }
            }
        }
    }

    // Spawn the ball
    void SpawnBall()
    {
        // Start background music in a loop
        audioSource.loop = true;
        audioSource.clip = m_AudioBackground;
        audioSource.Play();

        // Clean "any key" screen
        uiDisplay.CleanDisplay();

        // Enable camera control for the ball
        cameraControl.enabled = true;

        // Create the ball
        ballInstance = Instantiate(m_BallPrefab, m_StartingPosition.position, m_StartingPosition.rotation) as GameObject;

        // Set script used to push ball
        ballMovement = ballInstance.GetComponent<BallMovement>();
        ballMovement.SetCamera(m_Camera);
        ballMovement.SetPower(m_ChargeTime, m_Force);
        ballMovement.SetHole(m_Hole);
        ballMovement.SetColliders(m_OutOfBoundsColliders);
        ballMovement.SetManager(this);
 
        // Set slider
        uiSlider = ballInstance.GetComponentInChildren<UISliderControl>();
        uiSlider.SetCamera(m_Camera);
        uiSlider.SetRigidbody(ballInstance.GetComponent<Rigidbody>());

        // Give the ball instance to the camera so that it can track it
        cameraControl.SetPlayer(ballInstance);
    }

    public void CountStrokes(uint strokes)
    {
        uiDisplay.DisplayStrokes(strokes);
    }

    public void OutOfBounds()
    {
        uiDisplay.DisplayOutOfBounds();
    }

    public void EnableBall(bool status)
    {
        ballMovement.EnableMovement(status);
    }

    // Stop the game if ball is in hole
    public void End(uint strokes)
    {
        // Disable control on ball
        EnableBall(false);

        // Play ending sound once
        audioSource.loop = false;
        audioSource.volume = 1.0f;
        audioSource.clip = m_AudioCompleted;
        audioSource.Play();

        // Display ending message
        uiDisplay.DisplayEnd(strokes);
        Debug.Log("End of game, strokes "+strokes);
    }

    // If we resume from the menu instead of pressing the menu key
    public void ResumeFromMenu()
    {
        unpaused = 1;
        Time.timeScale = unpaused;

        // Reenable control on the ball
        EnableBall(true);

        // Resume background music
        audioSource.Play();
    }

    // If background music volume has been changed from menu
    public void MusicVolumeUpdate(float volume)
    {
        audioSource.volume = volume;
    }
}
