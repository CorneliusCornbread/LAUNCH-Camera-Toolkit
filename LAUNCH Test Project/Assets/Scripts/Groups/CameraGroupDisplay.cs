using UnityEngine;

namespace CameraToolkit.Groups
{
	public class CameraGroupDisplay : MonoBehaviour
    {
        [SerializeField] 
        private CameraGroup group;
        
        [SerializeField]
        private Renderer render;

        private Texture2D _oldTex;
        
        private void FixedUpdate()
        {
            Camera cam = group.ActiveGroupedCamera.Camera;

            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = cam.targetTexture;
            RenderTexture targetTexture = cam.targetTexture;

            cam.Render();

            Destroy(_oldTex);
            Texture2D image = new Texture2D(targetTexture.width, targetTexture.height);
            image.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
            image.Apply();

            render.material.mainTexture = image;

            RenderTexture.active = currentRT;

            _oldTex = image;
        }
    }
}