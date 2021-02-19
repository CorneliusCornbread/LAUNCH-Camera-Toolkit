using UnityEngine;

namespace CameraToolkit
{
	public class CameraMoveTester : MonoBehaviour
	{
		[SerializeField]
		private Interpolator cam;

		[SerializeField]
		private Vector3 position;

		[SerializeField]
		private Vector3 rotation;

        private void Update()
        {
			cam.targetPosition = position;
        }
    }
}