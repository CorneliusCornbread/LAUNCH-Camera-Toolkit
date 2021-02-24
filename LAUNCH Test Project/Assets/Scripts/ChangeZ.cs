using UnityEngine;

namespace CameraToolkit
{
	public class ChangeZ : MonoBehaviour
	{

		public void OnButtonZForwardPressed()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y,
				transform.position.z + 1);
		}

		public void OnButtonZBackPressed()
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
		}
	}
}