using System.Collections;
using UnityEngine;

namespace CameraToolkit
{
    public class Interpolator : MonoBehaviour
    {
        public bool interpolatePosition = false;
        public bool interpolateRotation = false;

        [SerializeField]
        [Tooltip("How fast the position interpolation will move in meters per second.")]
        [Range(.1f, 10)]
        private float positionInterpSpeed = 0.25f;

        [Tooltip("The number of times per second the target position is updated. Default is 60.")]
        [Range(1, 240)]
        public uint updatesPerSecond = 60;

        /// <summary>
        /// The target position to move to
        /// </summary>
        public Vector3 targetPosition;

        /// <summary>
        /// The target rotation to move to
        /// </summary>
        public Quaternion targetRotation;

        private Vector3 _currentPosTarget;
        private Quaternion _currentRotTarget;

        private Vector3 _startPosition;
        private Quaternion _startRotation;

        //Used to scale the speed as the interpolation distance increases or decreases
        private float _targetDist;
        private float _rotationInterpScale;

        //Percent interpolation
        private float _positionInterp;
        private float _rotationInterp;

        private void Start()
        {
            StartCoroutine(Interpolate());
        }

        private void Update()
        {
            if (interpolatePosition)
            {
                _positionInterp += (Time.deltaTime * positionInterpSpeed) / _targetDist;
                //_positionInterp *= GetEasing(_positionInterp);

                //TODO: setup easing

                transform.position = Vector3.Lerp(_startPosition, _currentPosTarget, _positionInterp);
            }
        }

        private IEnumerator Interpolate()
        {
            while (true)
            {
                if (!enabled || !gameObject.activeInHierarchy) { continue; } //Coroutines can run regardless of a script or game object being enabled

                float updateDelay = 1 / updatesPerSecond; //We do this calculation every coroutine loop as it could change at runtime
                yield return new WaitForSeconds(updateDelay);

                if (interpolatePosition)
                {
                    _startPosition = transform.position;
                    _positionInterp = 0;

                    _currentPosTarget = targetPosition;
                    _targetDist = Vector3.Distance(_startPosition, targetPosition);
                }

                if (interpolateRotation)
                {
                    _startRotation = transform.rotation;
                    _rotationInterp = 0;

                    _currentRotTarget = targetRotation;
                }
            }
        }

        /// <summary>
        /// A function used to ease the interpolation
        /// </summary>
        /// <param name="interp">Current interpolation value</param>
        /// <returns></returns>
        private float GetEasing(float interp)
        {
            return Mathf.Sin((2.5f * Mathf.Clamp(interp, 0, 1)) + .32f) * 2;
        }
    }
}