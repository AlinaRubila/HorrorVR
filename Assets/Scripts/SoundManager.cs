using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> ambients;
    [SerializeField] List<AudioClip> footsteps;
    string[] places = new string[]{"HubScene", "MazeScene","HallScene", "GhostScene" };
    [SerializeField] AudioSource player;
    [SerializeField] AudioSource background;
    [SerializeField] AudioMixer mixer;
    public void ChangeSounds(string place)
    {
        background.clip = ambients[Array.IndexOf(places, place)];
        player.clip = footsteps[Array.IndexOf(places, place)];
        background.Play();
    }
    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    public void SetVolume(float volume)
    {
        mixer.SetFloat("ambientVolume", volume);
    }
    public void ChangeEffects(float value) { }
}
