using System.Collections;
using UnityEngine;

    public class AudioManager: MonoBehaviour
    {
        public static AudioManager Instance;
        public AudioSource audioSource;

        [Range(0, 1)]
        public float maximumVolume;

        private AudioClip _pendingAudioClip;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(gameObject);

            audioSource.volume = maximumVolume;
        }

        public void SetAudioSourceVolume(float userMultiplier)
        {
            audioSource.volume = maximumVolume * userMultiplier;
        }

        public void PlayClip(AudioClip newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }

        public void PauseAudio()
        {
            audioSource.Pause();
        }

        public void ResumeAudio()
        {
            audioSource.Play();
        }

        public void ChangeAudioClip(AudioClip newClip)
        {
            _pendingAudioClip = newClip;
            StartCoroutine(SwitchAudioClipWithFade());
            _pendingAudioClip = null;
        }

        public void ChangeAudioClip(string pathToAudioClip)
        {
            _pendingAudioClip = Resources.Load<AudioClip>(pathToAudioClip);
            StartCoroutine(SwitchAudioClipWithFade());
            _pendingAudioClip = null;
        }

        public IEnumerator FadeSound()
        {
            const float fadeTime = 1f;
            float t = 0f;
            float initialVolume = audioSource.volume;

            while (t < 1)
            {
                t += Time.deltaTime/fadeTime;
                audioSource.volume = Mathf.Lerp(initialVolume, 0.00f, t);
                yield return null;
            }
        }

        private IEnumerator SwitchAudioClipWithFade()
        {
            const float fadeTime = 0.7f;
            float t = 0f;
            float initialVolume = audioSource.volume;

            while (t < 1)
            {
                t += Time.deltaTime / fadeTime;
                audioSource.volume = Mathf.Lerp(initialVolume, 0.00f, t);
                yield return null;
            }

            audioSource.Stop();
            audioSource.clip = _pendingAudioClip;
            audioSource.Play();
            t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / fadeTime;
                audioSource.volume = Mathf.Lerp(0.00f, initialVolume, t);
                yield return null;
            }
        }
    }
