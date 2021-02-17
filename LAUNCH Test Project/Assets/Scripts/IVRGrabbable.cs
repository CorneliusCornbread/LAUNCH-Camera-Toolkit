using UnityEngine;

namespace CameraToolkit
{
	public interface IVRGrabbable
	{
		/// <summary>
		/// Called when the user firsts grabs the object
		/// </summary>
		void Grab();

		/// <summary>
		/// Called when the user releases the object
		/// </summary>
		void Release();

		/// <summary>
		/// Called every time the position changes
		/// </summary>
		/// <param name="position">New position</param>
		/// <param name="rotation">New rotation</param>
		void Move(Vector3 position, Quaternion rotation);
	}
}