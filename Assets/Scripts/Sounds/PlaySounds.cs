using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class PlaySounds : MonoBehaviour
{
    private AudioSource collisionSource;
    private AudioSource loadingSource;
    public AudioClip m_Collision;
    public AudioClip m_Loading_Slider;
    public AudioMixerGroup m_AudioMixer;

    void Start()
    {
        collisionSource = gameObject.AddComponent<AudioSource>();
        collisionSource.clip = m_Collision;
        collisionSource.volume = 1.0f;
        collisionSource.outputAudioMixerGroup = m_AudioMixer;
        loadingSource = gameObject.AddComponent<AudioSource>();
        loadingSource.clip = m_Loading_Slider; 
        loadingSource.loop = true;
        loadingSource.volume = 0.1f;
        loadingSource.outputAudioMixerGroup = m_AudioMixer; 
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
        loadingSource.Play();
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
