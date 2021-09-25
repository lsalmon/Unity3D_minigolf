using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BallManager
{
    public Transform m_StartingPosition;
    public GameObject m_Hole;
    private GameObject m_Manager;
    public float m_ChargeTime = 2.0f;
    public float m_Force = 10.0f;
    private BallMovement m_Movement;

    private UISliderControl ui;
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
        ui = m_Instance.GetComponentInChildren<UISliderControl>();
        ui.SetCamera(camera);
        ui.SetRigidbody(m_Instance.GetComponent<Rigidbody>());
    }

    public void End(uint strokes)
    {
        Debug.Log("In ballmanager, strokes "+strokes);
        m_Manager.SendMessage("End", strokes);
    }
}
