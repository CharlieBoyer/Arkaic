using System;
using UnityEngine;

namespace Canon
{
    public class Ammunition: MonoBehaviour
    {
        [SerializeField] [Range(0,5)]
        private int _piercing;
        
        private float _lifetime = 0f;
        private const float MaximumLifetime = 30f;

        private void FixedUpdate()
        {
            _lifetime += Time.deltaTime;
            
            if (_lifetime > MaximumLifetime)
                Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlaygroundLimit")) {
                Destroy(this.gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Brick")) {
                Debug.Log("Hit");
                Brick brick = other.gameObject.GetComponent<Brick>();
                brick.LooseDurability(_piercing);
            }
        }
    }
}
