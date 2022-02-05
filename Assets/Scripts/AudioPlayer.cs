using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource _musicLoop;
    
    // [Header("Music")]
    // [SerializeField] private AudioClip baseMusic;
    // [SerializeField] [Range(0.0f, 1.0f)] private float musicVolume = 1.0f;
    
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] [Range(0.0f, 1.0f)] private float shootingVolume = 1.0f;

    [Header("On Damage")]
    [SerializeField] private AudioClip damageClip;
    [SerializeField] [Range(0.0f, 1.0f)] private float damageVolume = 1.0f;

    private void Awake()
    {
        _musicLoop = GetComponent<AudioSource>();
        if (FindObjectsOfType<AudioPlayer>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartMusic()
    {
        if (_musicLoop == null) return;
        
        _musicLoop.Play();
    }

    public void StopMusic()
    {
        if (_musicLoop == null) return;
        
        _musicLoop.Stop();
    }
    
    public void PlayShootingClip()
    {
        PlayClip(ref shootingClip, ref shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(ref damageClip, ref damageVolume);
    }

    private void PlayClip(ref AudioClip clip, ref float volume)
    {
        if (clip == null || Camera.main == null) return;
        
        AudioSource.PlayClipAtPoint(clip,
            Camera.main.transform.position,
            volume);
    }
}
