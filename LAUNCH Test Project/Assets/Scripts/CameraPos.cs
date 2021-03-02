using UnityEngine;
using UnityEngine.UI;

namespace CameraToolkit
{
	public class CameraPos : MonoBehaviour
	{
        public Interpolator optionalInterpolator;
        public Button forwardButton;
        public Button backwardsButton;
        public int forwardDistance;
        public int backwardsDistance;
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
        ///Vector3 endPos;
        ///Vector3 startPos;
        ///float lerpMult;
        private void Start()
        {
            Button btn1 = forwardButton;
            btn1.onClick.AddListener(CamForward);

            Button btn2 = backwardsButton;
            btn2.onClick.AddListener(CamBackwards);
        }

        void CamForward()
        {
            AddPosition(Camera.main.transform.forward * forwardDistance);
        }

        void CamBackwards()
        {
            AddPosition(Camera.main.transform.forward * -backwardsDistance);
        }
     
    }
}