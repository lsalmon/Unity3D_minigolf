using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BallManager
{
    public Transform m_StartingPosition;
    public GameObject m_Hole;
    public Canvas m_Display;
    private GameObject m_Manager;
    public float m_ChargeTime = 2.0f;
    public float m_Force = 10.0f;
    private BallMovement m_Movement;

    private UISliderControl uislider;
    [HideInInspector] public GameObject m_Instance;

    public void Setup (Camera camera, GameObject gamemanager)
    {
        // Get ref to GameManager
        m_Manager = gamemanager;

        // Set script used to push ball
        m_Movement = m_Instance.GetComponent<BallMovement>();
        m_Movement.SetCamera(camera);
        m_Movement.SetPower(m_ChargeTime, m_Force);
        m_Movement.SetHole(m_Hole);
        m_Movement.SetManager(this);
 
        // Set slider
        uislider = m_Instance.GetComponentInChildren<UISliderControl>();
        uislider.SetCamera(camera);
        uislider.SetRigidbody(m_Instance.GetComponent<Rigidbody>());
    }

    public void CountStrokes(uint strokes)
    {
        m_Display.SendMessage("DisplayStrokes", strokes);
    }

    public void End(uint strokes)
    {
        m_Display.SendMessage("DisplayEnd", strokes);
        Debug.Log("In ballmanager, strokes "+strokes);
        m_Manager.SendMessage("End", strokes);
    }
}
