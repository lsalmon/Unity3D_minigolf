using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BallManager
{
    public Transform m_StartingPosition;
    private BallMovement m_Movement;

    private UISliderControl ui;
    [HideInInspector] public GameObject m_Instance;

    public void Setup (Camera camera)
    {
        m_Movement = m_Instance.GetComponent<BallMovement>();
        ui = m_Instance.GetComponentInChildren<UISliderControl>();
        m_Movement.SetCamera(camera);
        ui.SetCamera(camera);
        ui.SetRigidbody(m_Instance.GetComponent<Rigidbody>());
    }
}
