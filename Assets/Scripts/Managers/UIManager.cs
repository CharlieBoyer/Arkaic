using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public static bool pauseActive = false;
        
        public GameObject pauseOverlay;
        public Slider volume;
        public TMP_Text volumeValue;
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(this.gameObject);
        }

        private void Start() {
            pauseOverlay.SetActive(false);
        }

        public void Pause() {
            pauseActive = true;
            Time.timeScale = 0;
            pauseOverlay.SetActive(true);
        }

        public void Resume() {
            pauseActive = false;
            Time.timeScale = 1;
            pauseOverlay.SetActive(false);
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level01", LoadSceneMode.Single);
        }

        public void Quit() {
            SceneLoader.Exit();
        }

        public void UpdateVolume()
        {
            AudioManager.instance.SetAudioSourceVolume(volume.value);
            volumeValue.text = volume.value + " %";
        }
    }
}
