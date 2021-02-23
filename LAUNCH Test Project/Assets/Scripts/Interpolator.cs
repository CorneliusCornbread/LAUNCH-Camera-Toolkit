using System.Collections;
using UnityEngine;

namespace CameraToolkit
{
    public class Interpolator : MonoBehaviour
    {
        [Tooltip("The number of times per second the target position is updated. Default is 60. A lower number could improve performance.")]
        [Range(1, 240)]
        public uint updatesPerSecond = 60;

        #region Position Serialized Fields
        [Space(20)]
        public bool interpolatePosition = true;

        [SerializeField]
        [Tooltip("How fast the position interpolation will move in percent per second. This means the effective speed will change with the distance.")]
        [Range(.1f, 10)]
        private float positionInterpSpeed = 4;

        [SerializeField]
        [Tooltip("If the distance is closer than this then the interpolator will snap to it")]
        [Range(.001f, .025f)]
        private float errorPosSnapDist = .008f;

        /// <summary>
        /// The target position to move to
        /// </summary>
        public Vector3 targetPosition;
        #endregion

        #region Rotation Serialized Fields
        [Space(20)]
        public bool interpolateRotation = true;

        [SerializeField]
        [Tooltip("How fast the rotation interpolation will move in percent per second. This means the effective speed will change with the distance.")]
        [Range(.1f, 10)]
        private float rotationInterpSpeed = 4;

        [SerializeField]
        [Tooltip("If the distance is closer than this then the interpolator will snap to it")]
        [Range(.001f, .025f)]
        private float errorRotSnapDist = .025f;

        /// <summary>
        /// The target rotation to move to
        /// </summary>
        public Quaternion targetRotation;
        #endregion

        #region Private Non-Serialized Fields
        private Vector3 _currentPosTarget;
        private Quaternion _currentRotTarget;

        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private float _targetPosDist;
        private float _targetRotDist;

        //Percent interpolation
        private float _positionInterp;
        private float _rotationInterp;
        #endregion

        //We use OnEnable as disabling the parent game object will stop this coroutine
        private void OnEnable()
        {
            StartCoroutine(Interpolate());
        }

        private void Update()
        {
            if (interpolatePosition && _targetPosDist > errorPosSnapDist)
            {
                _positionInterp += Time.deltaTime * positionInterpSpeed;

                transform.position = Vector3.Lerp(_startPosition, _currentPosTarget, _positionInterp);
            }
            if (interpolateRotation && _targetRotDist > errorRotSnapDist)
            {
                _rotationInterp += Time.deltaTime * rotationInterpSpeed;

                transform.rotation = Quaternion.Slerp(_startRotation, _currentRotTarget, _rotationInterp);
            }
        }

        private IEnumerator Interpolate()
        {
            while (true)
            {
                //Coroutines can run regardless of a script or game object being enabled
                if (!enabled) { continue; } 

                //We do this calculation every coroutine loop as it could change at runtime
                float updateDelay = 1 / Mathf.Clamp(updatesPerSecond, .00001f, Mathf.Infinity); 
                yield return new WaitForSeconds(updateDelay);

                if (interpolatePosition)
                {
                    _targetPosDist = Vector3.Distance(_startPosition, targetPosition);

                    if (_targetPosDist > errorPosSnapDist) 
                    {
                        _startPosition = transform.position;
                        _positionInterp = 0;

                        _currentPosTarget = targetPosition;
                    }
                    else
                    {
                        _startPosition = targetPosition;
                        _currentPosTarget = targetPosition;
                        transform.position = targetPosition;
                    }
                }
                if (interpolateRotation)
                {
                    _targetRotDist = Quaternion.Angle(_startRotation, targetRotation) / 2;

                    if (_targetRotDist > errorRotSnapDist)
                    {
                        _startRotation = transform.rotation;
                        _rotationInterp = 0;

                        _currentRotTarget = targetRotation;
                    }
                    else
                    {
                        _startRotation = targetRotation;
                        _currentRotTarget = targetRotation;
                        transform.rotation = targetRotation;
                    }
                }
            }
        }
    }
}