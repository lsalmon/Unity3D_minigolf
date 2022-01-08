using UnityEngine;
using System.Collections;

public class PlaySounds : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Play collision sound every time we hit a collider (meshes for the ground, sides, etc)
    // The ball is always in collision with the ground except when it touches the sides or the flag,
    // or when it flies and falls back on the ground
    void OnCollisionEnter()
    {
        audioSource.Play();
    }
}
