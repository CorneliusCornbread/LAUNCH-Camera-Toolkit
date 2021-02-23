using System.Collections.Generic;
using UnityEngine;

namespace CameraToolkit
{
	public class MultiCameraManager : MonoBehaviour
	{
		[SerializeField]
		private List<Camera> cameras;

		public List<Camera> Cameras => cameras;

        private void Start()
        {
            
        }
    }
}