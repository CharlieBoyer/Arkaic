using UnityEngine;

namespace Canon
{
    public class FireSystem: MonoBehaviour
    {
        [Header("References")]
        public GameObject ammoPrefab;
        public Transform canonTip;
        private Vector3 _spawnWorldPosition;
        
        [Header("Properties")]
        public int firePower;
        private const int InherentFirePower = 10;

        private void Awake()
        {
            firePower *= InherentFirePower;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && !Ammunition.isAlive) {
                Fire();
            }
        }

        private void Fire()
        {
            GameObject ammoClone = Instantiate(ammoPrefab, canonTip);
            
            canonTip.DetachChildren();
            ammoClone.GetComponent<Rigidbody>().AddForce(ammoClone.transform.up * firePower, ForceMode.Impulse);
        }
        
    }
}
