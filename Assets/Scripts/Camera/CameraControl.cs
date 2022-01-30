using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> env;

    public Camera m_Camera;
    public Vector3 m_CamOffset   = new Vector3(0.5f, -0.5f, 0.0f);
    public Vector3 m_CamRotation = new Vector3(60.0f, 0.0f, 0.0f);
    public float aimingSpeed = 450f;
    
    private Vector3 offset;
    private float playerAngle;
    private float playerDist;
    private Canvas slider;

    // Awake runs before Start
    void Awake()
    {
        // Set initial camera postion and rotation
        m_Camera = transform.GetComponent<Camera>();
        
//        transform.position = transform.position + m_CamOffset;
        transform.eulerAngles = m_CamRotation;
        playerAngle = transform.eulerAngles.y;
    }

    public void SetPlayer(GameObject ball)
    {
        player = ball;
        slider = player.GetComponentInChildren<Canvas>();
    }

    void Start()
    {
        // Get distance between player and camera
//        offset = player.transform.position - transform.position;
    }

    void Update ()
    {
        RaycastHit hitInfo;

        if (Physics.Linecast(transform.position, slider.transform.position, out hitInfo))
        {
            if (hitInfo.collider.name != "Ground")
            {
                foreach (var obj in env)
                {
                    if (obj.name == hitInfo.collider.name || (obj.name == "Sides" && hitInfo.collider.name == "Top"))
                    {
                        obj.GetComponent<SetOpacity>().ChangeOpacity(true);
                    }
                    else
                    {
                        obj.GetComponent<SetOpacity>().ChangeOpacity(false);
                    }
                }
            }
        }
    }

    // LateUpdate is called after all Update methods
    // This way the camera can follow objects which have moved during Update
    void LateUpdate ()
    {
        // Get player input 
        playerAngle += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * aimingSpeed * Time.deltaTime;
        // Get distance between camera and player in a float
        playerDist = Vector3.Distance(transform.position, transform.position - m_CamOffset);

        Quaternion rotation = Quaternion.Euler(0, playerAngle, 0);

        // Move camera
        transform.position = player.transform.position - (rotation * m_CamOffset);
//        transform.position = transform.position + m_CamOffset;

        // Point camera at player
        transform.LookAt(player.transform);
    }

}
