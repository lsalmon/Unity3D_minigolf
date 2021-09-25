using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject m_BallPrefab;
    public Camera m_Camera;
    public BallManager m_Ball;
    private CameraControl m_CameraControl;

    // Spawn the ball
    void Start()
    {
        m_Ball.m_Instance = Instantiate(m_BallPrefab, m_Ball.m_StartingPosition.position, m_Ball.m_StartingPosition.rotation) as GameObject;
        m_Ball.Setup(m_Camera, this.gameObject);
        m_CameraControl = m_Camera.GetComponent<CameraControl>();
        m_CameraControl.SetPlayer(m_Ball.m_Instance);
    }

    // Stop the game if ball is in hole
    void End(uint strokes)
    {
        Debug.Log("End of game, strokes "+strokes);
    }
}
