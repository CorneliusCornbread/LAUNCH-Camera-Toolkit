using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody rb;

    [SerializeField]
    private int speed = 500;

    [SerializeField]
    private float maxVelocityChange = 4;

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3();
        targetVelocity.x = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * speed;
        targetVelocity.z = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime * speed;

        Vector3 velocityChange = targetVelocity - rb.velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange * Time.fixedDeltaTime, maxVelocityChange * Time.fixedDeltaTime);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange * Time.fixedDeltaTime, maxVelocityChange * Time.fixedDeltaTime);

        rb.AddForce(velocityChange, ForceMode.Impulse);
    }
}
