using System.Collections.Generic;
using UnityEngine;

namespace CameraToolkit
{
    [DisallowMultipleComponent]
	public class MultiCameraManager : MonoBehaviour
	{
        /// <summary>
        /// The currently active singleton instance
        /// </summary>
        public static MultiCameraManager Instance { get; private set; }

        [SerializeField]
        private Interpolator optionalInterpolator;

		[SerializeField]
		private List<MultiCamera> cameras;

        public int ActiveCameraIndex { get; private set; } = -1;

        public int CameraCount => cameras.Count;

        private void Start()
        {
            if (Instance != null)
            {
                Debug.LogError("Cannot have more than one instance of MultiCameraManager active at once." +
                    $"Disabling 2nd instance \"{gameObject.name}\"");
                enabled = false;
                return;
            }
            else
            {
                Instance = this;

                //Update the serialized cameras to have the correct index
                for (int i = 0; i < cameras.Count; i++)
                {
                    cameras[i].cameraIndex = i;
                }
            }
        }

        /// <summary>
        /// Changes the active camera to the camera at the given collection index.
        /// </summary>
        /// <param name="cameraIndex">Camera to change to</param>
        public void ChangeActiveCamera(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex > cameras.Count - 1)
            {
                throw new System.ArgumentOutOfRangeException(nameof(cameraIndex));
            }

            if (ActiveCameraIndex > -1)
            {
                cameras[ActiveCameraIndex].SetCameraActive(false);
            }

            cameras[cameraIndex].SetCameraActive(true);
            ActiveCameraIndex = cameraIndex;
        }

        /// <summary>
        /// Changes the active camera to the given camera. Will error out if this camera hasn't been added yet.
        /// </summary>
        /// <param name="cam"></param>
        public void ChangeActiveCamera(MultiCamera cam)
        {
            if (cam == null)
            {
                throw new System.ArgumentNullException(nameof(cam));
            }

            if (cam.cameraIndex < 0)
            {
                Debug.LogError("Camera passed in ChangeActiveCamera has not been assigned an valid index." +
                    "Has this item not been added to the MultiCameraManager yet?");
                return;
            }

            ChangeActiveCamera(cam.cameraIndex);
        }

        /// <summary>
        /// Add a camera to our multi camera collection.
        /// </summary>
        /// <param name="cam"></param>
        public void AddCamera(MultiCamera cam)
        {
            if (cam == null)
            {
                throw new System.ArgumentNullException(nameof(cam));
            }

            cameras.Add(cam);
            cam.cameraIndex = cameras.Count - 1;
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            //Auto grab the interpolator component on this game object if it exists
            optionalInterpolator = GetComponent<Interpolator>();
        }
#endif
    }
}