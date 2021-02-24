using UnityEngine;

namespace CameraToolkit
{
	public class ChangeY : MonoBehaviour
	{

		public void OnButtonYUpPressed()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + 1,
				transform.position.z);
		}

		public void OnButtonYDownPressed()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - 1,
				transform.position.z);
		}
	}
}