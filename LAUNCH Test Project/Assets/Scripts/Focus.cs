using UnityEngine;

namespace CameraToolkit
{
	public class Focus : MonoBehaviour
	{
        [Tooltip("The target the camera should focus on.")]
		public Transform target;

        [SerializeField]
        [Tooltip("An optional interpolator component.")]
        private Interpolator optionalInterpolator;

        private void Update()
        {
            if (target == null) { return; }

            if (optionalInterpolator == null)
            {
                transform.LookAt(target.position);
            }
            else
            {
                optionalInterpolator.targetRotation = Quaternion.LookRotation(target.position - transform.position);
            }
        }
    }
}