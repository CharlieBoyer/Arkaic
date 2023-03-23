using UnityEngine;

namespace Canon
{
    public class FireSystem: MonoBehaviour
    {
        [Header("References")]
        public GameObject ammoPrefab;
        public Transform canonTip;
        private Vector3 spawnWorldPosition;

        [Header("Properties")]
        [SerializeField] private float _cooldown;
        private float _cooldownTimer;
        [SerializeField] private int _firePower;
        private readonly int _inherentFirePower = 10;

        private void Awake()
        {
            _firePower *= _inherentFirePower;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && _cooldownTimer <= 0) {
                Fire();
                Cooldown();
            }
            _cooldownTimer -= Time.deltaTime;
        }

        private void Fire()
        {
            GameObject ammoClone = Instantiate(ammoPrefab, canonTip);
            
            canonTip.DetachChildren();
            ammoClone.GetComponent<Rigidbody>().AddForce(ammoClone.transform.up * _firePower, ForceMode.Impulse);
        }

        private void Cooldown()
        {
            _cooldownTimer = _cooldown;
        }
    }
}
