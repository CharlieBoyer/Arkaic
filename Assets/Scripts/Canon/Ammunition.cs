using System;
using UnityEngine;

namespace Canon
{
    public class Ammunition: MonoBehaviour
    {
        [SerializeField] [Range(0,5)]
        private int _piercing;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlaygroundLimit")) {
                Destroy(this.gameObject);
            }
        }
    }
}
