using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;

    public Camera m_Camera;
    public Vector3 camOffset   = new Vector3(0.5f, -0.5f, 0.0f);
    public Vector3 camRotation = new Vector3(60.0f, 0.0f, 0.0f);
    public float aimingSpeed = 450f;
    
    private Transform camPosition;
    private Vector3 offset;
    private float playerAngle;
    private float playerDist;

    // Awake runs before Start
    void Awake()
    {
        // Set initial camera postion and rotation
        camPosition = transform;
        m_Camera = camPosition.GetComponent<Camera>();
        
//        transform.position = transform.position + camOffset;
        transform.eulerAngles = camRotation;
        playerAngle = transform.eulerAngles.y;
    }

    public void SetPlayer(GameObject ball)
    {
        player = ball;
    }

    void Start()
    {
        // Get distance between player and camera
//        offset = player.transform.position - transform.position;
    }
    
    // LateUpdate is called after all Update methods
    // This way the camera can follow objects which have moved during Update
    void LateUpdate ()
    {
        // Get player input 
        playerAngle += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * aimingSpeed * Time.deltaTime;
        // Get distance between camera and player in a float
        playerDist = Vector3.Distance(transform.position, transform.position - camOffset);
        //Debug.Log("PlayerDist " + playerDist);


        Quaternion rotation = Quaternion.Euler(0, playerAngle, 0);

        // Move camera
        transform.position = player.transform.position - (rotation * camOffset);
//        transform.position = transform.position + camOffset;

        // Point camera at player
        transform.LookAt(player.transform);
    }

}
