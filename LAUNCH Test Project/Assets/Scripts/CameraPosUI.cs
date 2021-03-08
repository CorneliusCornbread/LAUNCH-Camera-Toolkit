using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
    [RequireComponent(typeof(DistanceAdjuster))]
    [DisallowMultipleComponent]
	public class CameraPosUI : MonoBehaviour
	{
        [SerializeField]
        private DistanceAdjuster distAdjuster;

		[SerializeField]
		private Button forwardsButton;

		[SerializeField]
		private Button backwardsButton;

        [SerializeField]
        [Tooltip("The distance the camera will move forwards when the forwards button is pressed.")]
        private float forwardsDist = 5;

        [SerializeField]
        [Tooltip("The distance the camera will move back when the backwards button is pressed.")]
        private float backwardsDist = 5;

        private void Start()
        {
            forwardsButton.onClick.AddListener(ForwardsButton);

            backwardsButton.onClick.AddListener(BackwardsButton);
        }

		private void ForwardsButton()
        {
            distAdjuster.AdjustForward(forwardsDist);
        }

        private void BackwardsButton()
        {
            distAdjuster.AdjustBackwards(backwardsDist);
        }

        #region Editor Validation
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (distAdjuster == null)
            {
                //Grab the distance adjuster if it's not already set
                distAdjuster = GetComponent<DistanceAdjuster>();
            }
        }
#endif
        #endregion
    }
}