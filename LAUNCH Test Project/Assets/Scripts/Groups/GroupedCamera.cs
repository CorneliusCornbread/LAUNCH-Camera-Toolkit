using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace CameraToolkit.Groups
{
	/// <summary>
	/// A sort of interface class that makes managing multiple cameras easier. 
	/// Will automatically handle removing itself from the camera collection.
	/// </summary>
	[RequireComponent(typeof(Camera))]
	public class GroupedCamera : MonoBehaviour
	{
        #region Serialized Fields
        [SerializeField] 
        private CameraGroup cameraGroup;
        
        [SerializeField]
		private new Camera camera;

		[Tooltip("The index for which this MultiCamera is stored in the MultiCameraManager. This will be set during runtime.")]
		public int cameraIndex = -1;

		[SerializeField]
		[Tooltip("Should this managed camera auto register itself to the camera manager on start?")]
		private bool autoRegisterOnStart;

		[SerializeField]
		[Tooltip("Called when the camera has been enabled and is now the active camera.")]
		[Space(20)]
		private UnityEvent onCameraEnable;

		[SerializeField]
		[Tooltip("Called when the camera has been disabled and is no longer the active camera.")]
		private UnityEvent onCameraDisable;
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
		/// Called when the camera has been enabled and is now the active camera
		/// </summary>
		public UnityEvent OnCameraEnable => onCameraEnable;

		/// <summary>
		/// Called when the camera has been disabled and is no longer the active camera
		/// </summary>
		public UnityEvent OnCameraDisable => onCameraDisable;
		#endregion

        private void Start()
        {
	        if (!autoRegisterOnStart) return;
	        
	        Assert.IsNotNull(cameraGroup, "To use auto register on start a camera group must be assigned.");
	        cameraGroup.AddCamera(this);
        }

        internal void SetCameraActive(bool newState)
		{
			if (newState)
            {
				onCameraEnable.Invoke();
            }
            else
            {
				onCameraDisable.Invoke();
            }
        }

		#region Unity Editor Validation
#if UNITY_EDITOR
		[ContextMenu("Register Camera")]
		private void EditorRegisterCamera()
		{
			if (cameraGroup == null) return;

			cameraGroup.AddCamera(this);
		}


        private void OnValidate()
        {
			if (camera == null)
            {
				//Auto grab the camera component
				camera = GetComponent<Camera>();
			}
        }
#endif
        #endregion
    }
}