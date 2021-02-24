using UnityEngine;

namespace CameraToolkit
{
	[RequireComponent(typeof(Camera))]
	public class MultiCamera : MonoBehaviour
	{
        #region Serialized Fields
        [SerializeField]
		private new Camera camera;

		[SerializeField]
		private Interpolator optionalInterpolator;

		[Tooltip("The index for which this MultiCamera is stored in the MultiCameraManager. This will be set during runtime.")]
		public int cameraIndex = -1;
        #endregion

        #region Properties
        /// <summary>
        /// The camera attached to this MultiCamera
        /// </summary>
        public Camera Camera => camera;

		/// <summary>
		/// Is the current camera the active camera
		/// </summary>
		public bool IsCameraActive => camera.enabled;

		/// <summary>
		/// The target position the camera is trying to achieve.
		/// Will return the current position if there is no interpolator component made available to it.
		/// </summary>
		public Vector3 TargetPosition
        {
			get
            {
				if (optionalInterpolator != null)
                {
					return optionalInterpolator.targetPosition;
                }
                else
                {
					return transform.position;
                }
            }
        }

		/// <summary>
		/// The target rotation the camera is trying to achieve.
		/// Will return the current rotation if there is no interpolator component made available to it.
		/// </summary>
		public Quaternion TargetRoatation
		{
			get
			{
				if (optionalInterpolator != null)
				{
					return optionalInterpolator.targetRotation;
				}
				else
				{
					return transform.rotation;
				}
			}
		}
        #endregion

        /// <summary>
        /// Moves the camera to the given position in world space. 
        /// It will interpolate to that position if there is an interpolator component is available to it.
        /// </summary>
        /// <param name="position">Position in world space.</param>
        public void MoveCameraPosition(Vector3 position)
        {
			if (optionalInterpolator != null)
            {
				optionalInterpolator.targetPosition = position;
            }
            else
            {
				transform.position = position;
            }
        }

		/// <summary>
		/// Moves the camera's rotation to the give rotation.
		/// It will interpolate to that rotation if there is an interpolator component is available to it.
		/// </summary>
		/// <param name="rotation"></param>
		public void MoveCameraRotation(Quaternion rotation)
        {
			if (optionalInterpolator != null)
            {
				optionalInterpolator.targetRotation = rotation;
            }
            else
            {
				transform.rotation = rotation;
            }
        }

		public void SetCameraActive(bool newState)
        {
			camera.enabled = newState;

			if (newState)
            {
				
            }
            else
            {

            }
        }

#if UNITY_EDITOR
		private void OnValidate()
        {
			//Auto grab the camera component
			camera = GetComponent<Camera>();

			//Grab the interpolator component if it exists
			optionalInterpolator = GetComponent<Interpolator>();
        }
#endif
    }
}