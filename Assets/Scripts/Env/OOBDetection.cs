using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOBDetection : MonoBehaviour
{
    private bool debouncing = false;

    IEnumerator ResetBallWithDelay(BallMovement script, float time)
    {
         yield return new WaitForSeconds(time);
         script.ResetBall();
         debouncing = false;
    }

    // Reset position of the ball if it falls on the ground
    void OnCollisionEnter(Collision collision)
    {
        // Check that the object is the ball and it is not already being reset
        if (!debouncing && collision.gameObject.tag == "Player")
        {
            // Retrieve script for ball
            BallMovement ballScript = collision.gameObject.GetComponent<BallMovement>();
            if (ballScript != null)
            {
                debouncing = true;
                StartCoroutine(ResetBallWithDelay(ballScript, 0.5f));
            }
        }
    }
}
