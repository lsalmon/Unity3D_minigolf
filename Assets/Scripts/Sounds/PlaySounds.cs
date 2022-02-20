using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class PlaySounds : MonoBehaviour
{
    public AudioClip m_Collision;
    public AudioClip m_LoadingSlider;
    public AudioMixerGroup[] m_AudioMixers;
    private AudioSource collisionSource;
    private AudioSource loadingSource;

    void Start()
    {
        collisionSource = gameObject.AddComponent<AudioSource>();
        collisionSource.clip = m_Collision;
        collisionSource.volume = 1.0f;
        collisionSource.outputAudioMixerGroup = m_AudioMixers[0];
        loadingSource = gameObject.AddComponent<AudioSource>();
        loadingSource.clip = m_LoadingSlider;
        loadingSource.loop = true;
        loadingSource.volume = 1.0f;
        loadingSource.outputAudioMixerGroup = m_AudioMixers[1];
    }

    // Play collision sound every time we hit a collider (meshes for the ground, sides, etc)
    // The ball is always in collision with the ground except when it touches the sides or the flag,
    // or when it flies and falls back on the ground
    void OnCollisionEnter()
    {
        collisionSource.Play();
    }

    // Playing loading sound for the slider when player clicks
    public void StartLoading()
    {
        loadingSource.pitch = 1.0f;
        loadingSource.Play();
    }

    // Set pitch during loading
    public void SetPitch(float pitch)
    {
        // Pitch starts at 0.5f and ends at 1.5f
        loadingSource.pitch = 0.5f + pitch;
    }

    // End loading sound once player releases mouse
    public void EndLoading()
    {
        if (loadingSource.isPlaying)
        {
            loadingSource.Stop();
        }
    }
}
