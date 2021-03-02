using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class CameraPos : MonoBehaviour
	{
        [Tooltip("optionalInterpolator is for turning off/ on the interpolate function for the distance adjuster")]
        public Interpolator optionalInterpolator;
        public Button forwardButton;
        public Button backwardsButton;
        [Tooltip("Set the distance change for each click of the button in the forward direction (Positive value)")]
        public int forwardDistance;
        [Tooltip("Set the distance change for each click of the button in the backwards direction (Positive value)")]
        public int backwardsDistance;

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
            }}
       
        private void Start()
        {
            Button btn1 = forwardButton;
            btn1.onClick.AddListener(CamForward);

            Button btn2 = backwardsButton;
            btn2.onClick.AddListener(CamBackwards);
        }

        /// <summary>
        /// Functions that are assigned to the buttons
        /// </summary>
        private void CamForward()
        {
            AddPosition(Camera.main.transform.forward * forwardDistance);
        }

        private void CamBackwards()
        {
            AddPosition(Camera.main.transform.forward * -backwardsDistance);
        }
     
    }
}