using System;
using UnityEngine;

namespace CyberInfection.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class StartLoopMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip startClip;
        [SerializeField] private AudioClip loopClip;
        
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            PlayStart();

            Invoke(nameof(PlayLoop), startClip.length);
        }

        private void PlayStart()
        {
            audioSource.clip = startClip;
            audioSource.loop = false;
            audioSource.Play();
        }

        private void PlayLoop()
        {
            audioSource.clip = loopClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
