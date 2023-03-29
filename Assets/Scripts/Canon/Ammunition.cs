using System.Collections;
using UnityEngine;
using Managers;

namespace Canon
{
    public class Ammunition : MonoBehaviour
    {
        public static bool isAlive = false;
        
        [Range(0, 5)] public int piercing;

        private const float MaximumLifetime = 2f;
        private float _lifetime = 0f;

        private void Awake()
        {
            StartCoroutine(StartLifetime());
        }
        
        private IEnumerator StartLifetime()
        {
            isAlive = true;
            
            while (_lifetime < MaximumLifetime)
            {
                _lifetime += Time.deltaTime;
                yield return null;
            }
            
            Destroy(this.gameObject);
            isAlive = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlaygroundLimit"))
            {
                StopAllCoroutines();
                Destroy(this.gameObject);
                isAlive = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            _lifetime = 0f;

            if (other.gameObject.CompareTag("Brick"))
            {
                Brick brick = other.gameObject.GetComponent<Brick>();
                brick.LooseDurability(piercing);
                ScoreManager.instance.RegisterPoints(ScoreManager.instance.bounceValue);
            }
        }

        private void OnDestroy()
        {
            ScoreManager.instance.UpdateGlobalScore();
        }
    }
}
