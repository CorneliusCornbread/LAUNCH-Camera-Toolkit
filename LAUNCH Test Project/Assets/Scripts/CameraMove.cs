using UnityEngine;

namespace CameraToolkit
{
    public class CameraMove : MonoBehaviour, IVRGrabbable
    {
        public bool interpolatedMove = false;
        public float lerpMult = 1;

        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        private float _time = 0f;

        private void Update()
        {
            if (interpolatedMove)
            {
                _time += Time.deltaTime;
                float lerp = Mathf.Clamp(_time * lerpMult, 0, 1);

                transform.position = Vector3.Lerp(transform.position, _targetPosition, lerp);

                transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, lerp);

                if (lerp > .99f)
                {
                    _time = 0;
                    transform.position = _targetPosition;
                    transform.rotation = _targetRotation;
                }
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