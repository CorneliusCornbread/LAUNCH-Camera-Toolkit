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
		
        startPosition = transform.position.y+ (mainSlider.value * 10);

		transform.position = new Vector3(transform.position.x, 
		startPosition + Mathf.Sin(Time.time * speed), 
		transform.position.z);

		Debug.Log(mainSlider.value); // debug
    } 
}