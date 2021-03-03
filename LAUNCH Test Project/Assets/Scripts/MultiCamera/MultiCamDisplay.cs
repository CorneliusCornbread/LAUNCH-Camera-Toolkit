using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit.MultiCamera
{
	public class MultiCamDisplay : MonoBehaviour
	{
        [SerializeField]
        private Renderer render;

        private void FixedUpdate()
        {
            if (CameraManager.Instance.ActiveManagedCamera == null) { return; }

            Camera cam = CameraManager.Instance.ActiveManagedCamera.Camera;

            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = cam.targetTexture;

            cam.Render();

            Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
            image.Apply();

            render.material.mainTexture = image;

            RenderTexture.active = currentRT;
        }
    }
}