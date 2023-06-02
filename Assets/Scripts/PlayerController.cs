using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Debug")]
    public float throttle;
    public float steering;

    [Header("Speed Settings")]
    public float acceleration = 2.5f;
    public float maxSpeed = 6f;
    public float maxReverseSpeed = 3f;
    public float brakeForce = 1f;
    public float frictionForce = 0.4f;

    [Header("Direction Settings")]
    public float turnSpeed = 20f;
    public float turningThreshold = 0.3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        throttle = Input.GetAxis("Vertical");    // Get input for throttle (W for forward, S for reverse).
        steering = Input.GetAxis("Horizontal");  // Get input for steering (A for left, D for right).
        
        AlignSpeedAndDirection();

        Acceleration();

        Turning();
    }

    private void Acceleration()
    {
        if (throttle > 0 && rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.up * acceleration * throttle);
        }
        else if (throttle == 0)
        {
            rb.AddForce(-rb.velocity * frictionForce);
        }
        else if (throttle < 0)
        {
            if (MovingForward())
            {
                rb.AddForce(-rb.velocity * brakeForce);
            }
            else if (rb.velocity.magnitude < maxReverseSpeed)
            {
                rb.AddForce(transform.up * -acceleration * Mathf.Abs(throttle));
            }

        }
    }

    private void Turning()
    {
        if (rb.velocity.magnitude > turningThreshold)
        {
            if (MovingForward())
            {
                rb.angularVelocity = -steering * turnSpeed * rb.velocity.magnitude;
            }
            else
            {
                rb.angularVelocity = steering * turnSpeed * rb.velocity.magnitude;
            }
        }
        else
        {
            rb.angularVelocity = 0;
        }
    }

    private void AlignSpeedAndDirection()
    {
        if (rb.velocity.magnitude > 0)
        {
            if (MovingForward())
            {
                rb.velocity = transform.up * rb.velocity.magnitude;
            }
            else
            {
                rb.velocity = transform.up * -rb.velocity.magnitude;
            }
        }
    }

    private bool MovingForward()
    {
        return Vector2.Dot(rb.velocity, transform.up) > 0.2;
    }
}
