using System;
using UnityEngine;

namespace Canon
{
    public class CanonController : MonoBehaviour
    {
        private FireSystem _fireSystem;
        
        [Range(0.1f, 10f)] public float sensitivity;
        private readonly float _inherentSensitivity = 100f;
        
        [SerializeField] [Range(0,-80)] private float minAngle;
        [SerializeField] [Range(0,80)] private float maxAngle;
        
        private Vector3 _targetAngle;

        private void Update()
        {
            HandleRotation();
        }

        private void HandleRotation()
        {
            float inputX = Input.GetAxis("Mouse X") * sensitivity * _inherentSensitivity * Time.deltaTime;

            _targetAngle += new Vector3(0, 0, -inputX); // Correcting direction
            _targetAngle.z = Mathf.Clamp(_targetAngle.z, minAngle, maxAngle);
        
            transform.rotation = Quaternion.Euler(_targetAngle);
        }
    }
}
