using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CameraToolkit.Groups
{
    public class CameraGroup : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The index of the serialized camera to set as the active camera on start, " +
                 "is clamped to collection of cameras in the array. Set to -1 to disable.")]
        private int activeStartCameraIndex = -1;

        [SerializeField]
        private List<GroupedCamera> cameras = new List<GroupedCamera>();

        public int ActiveCameraIndex { get; private set; } = -1;

        public int CameraCount => cameras.Count;

        /// <summary>
        /// Gets the current active camera. Returns null if there is no active camera
        /// </summary>
        public GroupedCamera ActiveGroupedCamera
        {
            get
            {
                if (ActiveCameraIndex < 0)
                {
                    return null;
                }
                else
                {
                    return cameras[ActiveCameraIndex];
                }
            }
        }

        /// <summary>
        /// Indexes the camera collection within the CameraManager.
        /// </summary>
        /// <param name="i">Index</param>
        /// <returns>managed Cam at that index</returns>
        public GroupedCamera this[int i]
        {
            get { return cameras[i]; }
        }

        private void Start()
        {
            HashSet<GroupedCamera> hash = new HashSet<GroupedCamera>();

            //Add the cameras to a hash to remove duplicates
            for (int i = 0; i < cameras.Count; i++)
            {
                hash.Add(cameras[i]);
            }

            cameras = hash.ToList();

            //Update the serialized cameras to have the correct index
            for (int i = 0; i < cameras.Count; i++)
            {
                cameras[i].cameraIndex = i;
            }

            if (activeStartCameraIndex > -1 && cameras.Count != 0)
            {
                cameras[Mathf.Clamp(activeStartCameraIndex, 0, cameras.Count - 1)].SetCameraActive(true);
                ActiveCameraIndex = activeStartCameraIndex;
            }
        }

        /// <summary>
        /// Disables the currently active camera.
        /// </summary>
        public void DisableActiveCamera()
        {
            if (ActiveCameraIndex < 0) { return; }

            cameras[ActiveCameraIndex].SetCameraActive(false);
            ActiveCameraIndex = -1;
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
        public void ChangeActiveCamera(GroupedCamera cam)
        {
            if (cam == null)
            {
                throw new System.ArgumentNullException(nameof(cam));
            }

            if (cam.cameraIndex < 0)
            {
                Debug.LogError("Camera passed in ChangeActiveCamera() has not been assigned an valid index." +
                    "Has this item not been added to the CameraManager yet?");
                return;
            }

            ChangeActiveCamera(cam.cameraIndex);
        }

        /// <summary>
        /// Changes the active camera to the next camera in the managed camera collection.
        /// </summary>
        public void ChangeToNextCamera()
        {
            if (cameras.Count == 0)
            {
                Debug.LogWarning("Tried to change to the next camera in managed Cam when there are no cameras setup.");
                return;
            }

            int newInd = ActiveCameraIndex + 1;

            if (newInd > cameras.Count - 1)
            {
                ChangeActiveCamera(0);
            }
            else
            {
                ChangeActiveCamera(newInd);
            }
        }

        /// <summary>
        /// Changes the active camera to the previous camera in the managed camera collection.
        /// </summary>
        public void ChangeToPreviousCamera()
        {
            if (cameras.Count == 0)
            {
                Debug.LogWarning("Tried to change to the previous camera in managed Cam when there are no cameras setup.");
                return;
            }

            int newInd = ActiveCameraIndex + -1;

            if (newInd < 0)
            {
                ChangeActiveCamera(cameras.Count - 1);
            }
            else
            {
                ChangeActiveCamera(newInd);
            }
        }

        /// <summary>
        /// Add a camera to our managed camera collection.
        /// </summary>
        /// <param name="newCamera">Camera to add.</param>
        public void AddCamera(GroupedCamera newCamera)
        {
            if (newCamera == null)
            {
                throw new System.ArgumentNullException(nameof(newCamera));
            }

            if (cameras.Contains(newCamera))
            {
                Debug.LogWarning("Tried to add a managed camera to the camera manager when it's already been added.");
                return;
            }

            cameras.Add(newCamera);
            newCamera.SetCameraActive(false);
            newCamera.cameraIndex = cameras.Count - 1;
        }

        /// <summary>
        /// Removes a camera from the camera collection and disables it. Does not destroy the camera.
        /// </summary>
        /// <param name="cameraIndex">Index of camera</param>
        public void RemoveCamera(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex > cameras.Count - 1)
            {
                throw new System.ArgumentOutOfRangeException(nameof(cameraIndex));
            }

            cameras[cameraIndex].SetCameraActive(false);
            cameras[cameraIndex].cameraIndex = -1;
            cameras.RemoveAt(cameraIndex);

            //We have to reset the indexes on the cameras because if we remove anything other than the absolute last index 
            //it'll shift everything.
            for (int i = 0; i < cameras.Count; i++)
            {
                cameras[i].cameraIndex = i;
            }
        }

        /// <summary>
        /// Removes a camera from the camera collection and disables it. Does not destroy the camera.
        /// </summary>
        /// <param name="cam">Camera to remove</param>
        public void RemoveCamera(GroupedCamera cam)
        {
            if (cam == null)
            {
                throw new System.ArgumentNullException(nameof(cam));
            }

            if (cam.cameraIndex < 0)
            {
                Debug.LogError("Camera passed in RemoveCamera() has not been assigned an valid index." +
                    "Has this item not been added to the CameraManager yet?");
                return;
            }

            if (cam != cameras[cam.cameraIndex])
            {
                Debug.LogError("The managed camera does not match it's index in the collection. " +
                    "Has this camera's index been modified?");
                return;
            }

            RemoveCamera(cam.cameraIndex);
        }

        #region Editor Debugging
#if UNITY_EDITOR
        public static bool EDITOR_isExiting = false;

        [InitializeOnLoadMethod]
        static void EditorLoad()
        {
            EDITOR_isExiting = false;
            EditorApplication.playModeStateChanged += EditorPlayModeStateChanged;
        }

        private static void EditorPlayModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.ExitingPlayMode)
            {
                EDITOR_isExiting = true;
            }
        }

        [ContextMenu("Change To Next Camera")]
        private void EditorNextCam()
        {
            ChangeToNextCamera();
        }

        [ContextMenu("Change To Previous Camera")]
        private void EditorPreviousCam()
        {
            ChangeToPreviousCamera();
        }
#endif
        #endregion
    }
}