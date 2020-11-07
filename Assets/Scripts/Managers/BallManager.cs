using System;
using UnityEngine;

[Serializable]
public class BallManager
{
    public Transform m_StartingPosition;
    private BallMovement m_Movement;

    [HideInInspector] public GameObject m_Instance;

    public void Setup (Camera camera)
    {
        m_Movement = m_Instance.GetComponent<BallMovement>();
        m_Movement.SetCamera(camera);
    }
}
