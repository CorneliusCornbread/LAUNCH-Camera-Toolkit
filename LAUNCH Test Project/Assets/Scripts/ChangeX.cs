using UnityEngine;

namespace CameraToolkit
{
	public class ChangeX : MonoBehaviour
	{

		public void OnButtonXRightPressed()
		{
			transform.position = new Vector3(transform.position.x + 1, transform.position.y,
				transform.position.z);
		}

		public void OnButtonXLeftPressed()
		{
			transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
		}
	}
}