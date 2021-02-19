using UnityEngine;

namespace CameraToolkit
{
	public class CameraFocus : MonoBehaviour
	{
		public Transform target;

        private void Update()
        {
            Camera.main.transform.LookAt(target.position);
        }
    }
}