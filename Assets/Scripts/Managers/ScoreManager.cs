using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
        public enum Multiplier
        {
            None = 0,
            Increment = 1,
            LevelUp = 2
        }

        [Header("Score value management")]
        public int bounceValue;
        public int bonusValue;
        
        public static int globalScore = 0;
        public static int shotScore = 0;
        
        private static int _multiplier = 1;
        
        public static int multiplierProgress = 0;
        public static int multiplierUpThreshold = 3;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(this.gameObject);
        }

        public void UpdateGlobalScore()
        {
            int initialGlobalScore = globalScore;
            int initialShotScore = shotScore;
            
            ResetMultiplier();
            globalScore += shotScore;
            shotScore = 0;
            UIManager.instance.UpdateGlobalScore(initialGlobalScore, initialShotScore);
        }

        private void UpdateShotScore()
        {
            UIManager.instance.UpdateShotScore();
        }

        public void RegisterPoints(int points, Multiplier value = Multiplier.Increment)
        {
            shotScore += points * _multiplier;
            IncrementMultiplier(value);
            UpdateShotScore();
        }

        private void IncrementMultiplier(Multiplier multiplierGain)
        {
            int initialProgress = multiplierProgress;
            
            switch (multiplierGain)
            {
                case Multiplier.None:
                    break;
                case Multiplier.Increment:
                    multiplierProgress++;
                    break;
                case Multiplier.LevelUp:
                    multiplierProgress = multiplierUpThreshold;
                    break;
            }

            if (multiplierProgress == multiplierUpThreshold)
            {
                if (_multiplier < 10)
                    _multiplier++;
                UIManager.instance.IncreaseMultiplierLevel(_multiplier);
                multiplierProgress = 0;
                multiplierUpThreshold++;
            }
            else
            {
                UIManager.instance.UpdateMultiplierMeter(initialProgress);
            }
        }

        private void ResetMultiplier()
        {
            _multiplier = 1;
            multiplierProgress = 0;
            multiplierUpThreshold = 3;
            UIManager.instance.ResetMultiplierMeter();
            UIManager.instance.multiplierLevel.text = _multiplier.ToString();
        }
    }
}