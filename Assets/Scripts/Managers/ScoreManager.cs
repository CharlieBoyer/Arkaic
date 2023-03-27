using UnityEngine;

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
        public const int MultiplierUpThreshold = 10;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(this.gameObject);
        }

        public void UpdateGlobalScore()
        {
            
        }

        private void UpdateShotScore()
        {
            
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
                    multiplierProgress = MultiplierUpThreshold;
                    break;
            }

            if (multiplierProgress == MultiplierUpThreshold)
            {
                UIManager.instance.IncreaseMultiplierLevel();
            }
            else
            {
                UIManager.instance.UpdateMultiplierMeter(initialProgress);
            }
        }


    }
}
