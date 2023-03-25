using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameObject pauseOverlay;
        public Slider volume;
        public TMP_Text volumeValue;

        private void Start() {
            pauseOverlay.SetActive(false);
        }

        public void Pause() {
            Time.timeScale = 0;
            pauseOverlay.SetActive(true);
        }

        public void Resume() {
            Time.timeScale = 1;
            pauseOverlay.SetActive(false);
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level01", LoadSceneMode.Single);
        }

        public void Exit() {
            UnityEditor.EditorApplication.ExitPlaymode();
            // Application.Quit();
        }

        public void UpdateVolume()
        {
            AudioManager.instance.SetAudioSourceVolume(volume.value);
            volumeValue.text = volume.value + " %";
        }
    }
}
