using UnityEngine;

namespace CameraToolkit
{
	public class CameraMoveTester : MonoBehaviour
	{
		[SerializeField]
		private CameraMove cam;

		[SerializeField]
		private Vector3 position;

		[SerializeField]
		private Vector3 rotation;

        private void Update()
        {
			cam.Move(position, Quaternion.Euler(rotation));
        }
    }
}