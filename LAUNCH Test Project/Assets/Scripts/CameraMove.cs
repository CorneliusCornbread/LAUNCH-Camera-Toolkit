using UnityEngine;

namespace CameraToolkit
{
    public class CameraMove : MonoBehaviour, IVRGrabbable
    {
        public bool interpolatedMove = false;
        public float lerpMult = 1;

        private Vector3 _oldPosition;
        private Quaternion _oldRotation;

        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        private float _time = 0f;

        private void Update()
        {
            if (interpolatedMove)
            {
                _time += Time.deltaTime;
                transform.position = Vector3.Lerp(_oldPosition, _targetPosition, _time * lerpMult);

                transform.rotation = Quaternion.Lerp(_oldRotation, _targetRotation, _time * lerpMult);
            }
        }

        public void Grab()
        {
            
        }

        public void Release()
        {

        }

        public void Move(Vector3 position, Quaternion rotation)
        {
            if (interpolatedMove)
            {
                _oldPosition = _targetPosition;
                _oldRotation = _targetRotation;

                _targetPosition = position;
                _targetRotation = rotation;
            }
            else
            {
                transform.position = position;
                transform.rotation = rotation;
            }
        }
    }
}