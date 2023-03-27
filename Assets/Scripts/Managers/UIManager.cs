using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public static bool pauseActive;
        
        [Header("UI Pause Overlay")]
        public GameObject pauseOverlay;
        public Slider volume;
        public TMP_Text volumeValue;

        [Header("UI Score")]
        public TMP_Text globalScorePanel;
        public TMP_Text shotScorePanel;
        public Image multiplierMeter;
        public TMP_Text multiplierLevel;

        [Header("Animation delays")]
        [SerializeField] private float _scoreDelay;
        [SerializeField] private float _colorDelay;
        [SerializeField] private float _multiplierDelay;
        
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

        public void UpdateGlobalScore()
        {
            StartCoroutine(UpdateGlobalScoreAnimated());
            StartCoroutine(ResetShotScoreAnimated());
        }
        
        public void UpdateShotScore()
        {
            StartCoroutine(UpdateShotScoreAnimated(shotScorePanel, Color.yellow));
        }

        private IEnumerator UpdateGlobalScoreAnimated()
        {
            Color initialColor = globalScorePanel.color;
            float t = 0;
            int initialGlobalScore = ScoreManager.globalScore;
            int scoreToIncrement = initialGlobalScore + ScoreManager.shotScore;

            globalScorePanel.color = Color.green;
            
            while (t < 1)
            {
                t += Time.deltaTime / _scoreDelay;
                globalScorePanel.text = Mathf.RoundToInt(Mathf.Lerp(initialGlobalScore, scoreToIncrement, t)).ToString("D7");
                yield return null;
            }

            globalScorePanel.color = initialColor;

        }

        private IEnumerator ResetShotScoreAnimated()
        {
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime / _scoreDelay;
                shotScorePanel.text = Mathf.RoundToInt(Mathf.Lerp(ScoreManager.shotScore, 0, t)).ToString();
                yield return null;
            }
        }

        // ReSharper disable once SuggestBaseTypeForParameter (TMP_Text)
        private IEnumerator UpdateShotScoreAnimated(TMP_Text text, Color flashColor)
        {
            float t = 0f;
            Color startColor = text.color;

            while (t < 1) {
                t += Time.deltaTime / _colorDelay;
                text.color = Color.Lerp(startColor, flashColor, t);
                yield return null;
            }
            
            t = 0;
            shotScorePanel.text = ScoreManager.shotScore.ToString();
            
            while (t < 1) {
                t += Time.deltaTime / _colorDelay;
                text.color = Color.Lerp(flashColor, startColor, t);
                yield return null;
            }
        }

        public void UpdateMultiplierMeter(int initialProgress)
        {
            StartCoroutine(UpdateMultiplierMeterAnimated(initialProgress));
        }

        private IEnumerator UpdateMultiplierMeterAnimated(int initialProgress)
        {
            float t = 0f;
            float fillAmount = (float) initialProgress / ScoreManager.MultiplierUpThreshold;
            float targetAmount = (float) ScoreManager.multiplierProgress / ScoreManager.MultiplierUpThreshold;

            while  (t < 1f)
            {
                t += Time.deltaTime / _scoreDelay;
                multiplierMeter.fillAmount = Mathf.Lerp(fillAmount, targetAmount, t);
                yield return null;
            }

            multiplierMeter.fillAmount = targetAmount; // Interpolation imprecisions or rounding errors
        }

        public void IncreaseMultiplierLevel()
        {
            // Briefly increase font size and flash the whole meter before emptying it;
        }
    }
}
