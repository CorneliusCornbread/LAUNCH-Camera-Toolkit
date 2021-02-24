using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.


public class SliderJump : MonoBehaviour
{	
	[SerializeField]
	private Slider mainSlider;

	[SerializeField]
    private float speed = 9.0f;

	[SerializeField]
    private float startPosition;

	

    public void OnSliderMove()
    {
<<<<<<< HEAD
		
        startPosition = transform.position.y+ (mainSlider.value * 10);
=======
		int PositiveSliderValue = mainSlider.value * 500;
		
        startPosition = transform.position.y + (mainSlider.value * 10);
>>>>>>> Nick

		transform.position = new Vector3(transform.position.x, 
		startPosition + Mathf.Sin(Time.time * speed), 
		transform.position.z);
<<<<<<< HEAD

		Debug.Log(mainSlider.value); // debug
=======
>>>>>>> Nick
    } 
}