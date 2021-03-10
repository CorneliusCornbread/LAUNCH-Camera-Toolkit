using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
    /// <summary>
    /// A class which allows you to move an object forwards or backwards, dependent upon its direction.
    /// </summary>
    [DisallowMultipleComponent]
	public class DistanceAdjuster : MonoBehaviour
	{
        [Tooltip("optionalInterpolator is for turning off/ on the interpolate function for the distance adjuster")]
        public Interpolator optionalInterpolator;

        /// <summary>
        /// This is here for integration with the main interpolator script
        /// </summary>
        /// <param name="pos"></param>
        private void AddPosition(Vector3 pos)
        {
            if (optionalInterpolator != null)
            {
                optionalInterpolator.targetPosition += pos;
            }
            else
            {
                transform.position += pos;
            }
        }

        /// <summary>
        /// Moves the transform in the forwards direction, based on its rotation.
        /// </summary>
        /// <param name="distance">The distance to move closer to the camera.</param>
        public void AdjustForward(float distance)
        {
            AddPosition(transform.forward * distance);
        }

        /// <summary>
        /// Moves the transform in the backwards direction, based on its rotation.
        /// </summary>
        /// <param name="distance">The distance to move farther from the camera.</param>
        public void AdjustBackwards(float distance)
        {
            AddPosition(transform.forward * -distance);
        }
     
    }
}