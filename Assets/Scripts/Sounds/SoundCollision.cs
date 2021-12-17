using UnityEngine;
using System.Collections;

public class SoundCollision : MonoBehaviour
{
    public AudioClip collision;

    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;

        // Assign collision sound to source
        GetComponent<AudioSource>().clip = collision;
    }

    // Play collision sound every time we hit a collider (meshes for the ground, sides, etc)
    // The ball is always in collision with the ground except when it touches the sides or the flag,
    // or when it flies and falls back on the ground
    void OnCollisionEnter()
    {
        GetComponent<AudioSource>().Play();
    }
}
