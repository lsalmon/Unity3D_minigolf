using UnityEngine;
using System.Collections;

public class PlaySounds : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip background;
    public AudioClip collision;
    public AudioClip completed;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Assign background music to source
        audioSource.clip = background;
        audioSource.volume = 0.5f;

        audioSource.Play();
    }

    // Play collision sound every time we hit a collider (meshes for the ground, sides, etc)
    // The ball is always in collision with the ground except when it touches the sides or the flag,
    // or when it flies and falls back on the ground
    void OnCollisionEnter()
    {
        // Play collision sound once then go back to background music
        audioSource.PlayOneShot(collision);
    }

    public void Completed()
    {
        audioSource.clip = completed;
        audioSource.Play();
    }
}
