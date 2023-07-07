using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Debug")]
    public float throttle;
    public float steering;
    public float angle;

    [Header("Speed Settings")]
    public float acceleration = 2.5f;
    public float maxSpeed = 6f;
    public float maxReverseSpeed = 3f;
    public float brakeForce = 1f;
    public float frictionForce = 0.4f;

    [Header("Direction Settings")]
    public float turnSpeed = 20f;
    public float turningThreshold = 0.3f;

    [Header("Animation Settings")]
    [SerializeField]
    private Animator animator;
    [Tooltip("El angulo necesario para cambiar de sprite vertical a sprite de lado")]
    [Range(10, 45)]
    public float VerticalAmplitude = 30;

    [Header("Horn Settings")]
    [SerializeField]
    private float _hornCooldownDuration = 3f;
    private float _hornCooldownTime;
    private bool _hornInCooldown;

    private Rigidbody2D rb;

    private Direction previousDirection;


    [Header("Mobile Controls")]
    public bool isMobile;
    public TouchControl upButton;
    public TouchControl downButton;
    public TouchControl leftButton;
    public TouchControl rightButton;
    public Button hornButton;

    private void Awake()
    {
        if (!isMobile)
        {
            isMobile = Application.isMobilePlatform;
        }

        if (isMobile)
        {
            InitMobileControls();
        }

        rb = GetComponent<Rigidbody2D>();

        previousDirection = Direction.Up;
        SetAnimation(previousDirection, true);
    }

    private void OnDestroy()
    {
        upButton.OnPress -= () => SetVerticalInput(1);
        downButton.OnPress -= () => SetVerticalInput(-1);
        leftButton.OnPress -= () => SetHorizontalInput(-1);
        rightButton.OnPress -= () => SetHorizontalInput(1);

        upButton.OnRelease -= () => SetVerticalInput(0);
        downButton.OnRelease -= () => SetVerticalInput(0);
        leftButton.OnRelease -= () => SetHorizontalInput(0);
        rightButton.OnRelease -= () => SetHorizontalInput(0);

        hornButton.onClick.RemoveAllListeners();
    }

    void InitMobileControls()
    {
        //Debug.LogWarning("UI INPUT INIT");
        upButton.OnPress += () => SetVerticalInput(1);
        downButton.OnPress += () => SetVerticalInput(-1);
        leftButton.OnPress += () => SetHorizontalInput(-1);
        rightButton.OnPress += () => SetHorizontalInput(1);

        upButton.OnRelease += () => SetVerticalInput(0);
        downButton.OnRelease += () => SetVerticalInput(0);
        leftButton.OnRelease += () => SetHorizontalInput(0);
        rightButton.OnRelease += () => SetHorizontalInput(0);

        hornButton.onClick.AddListener(() => { SoundHorn(); });
    }

    void SetVerticalInput(float value)
    {
        //Debug.LogWarning("VERTICAL INPUT");
        throttle = value;
    }

    void SetHorizontalInput(float value)
    {
        //Debug.LogWarning("HORIZONTAL INPUT");
        steering = value;
    }

    private void Update()
    {
        if (!isMobile)
        {
            throttle = Input.GetAxis("Vertical");
            steering = Input.GetAxis("Horizontal");
        }

        AlignSpeedAndDirection();

        Acceleration();

        Turning();

        Animate();

        HornCooldown();

        SoundHornKey();
    }

    private void HornCooldown()
    {
        if (_hornInCooldown)
        {
            _hornCooldownTime += Time.deltaTime;
            if (_hornCooldownTime >= _hornCooldownDuration)
            {
                _hornCooldownTime = _hornCooldownDuration;
                _hornInCooldown = false;
            }
        }
    }

    private void SoundHornKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundHorn();
        }
    }

    private void SoundHorn()
    {
        if (!_hornInCooldown)
        {
            Messenger.Default.Publish(new HornSoundPayload(transform.position));
            Messenger.Default.Publish(new HornCooldownStartPayload(_hornCooldownDuration));
            _hornInCooldown = true;
            _hornCooldownTime = 0;
        }
    }

    private void Acceleration()
    {
        if (throttle > 0 && rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.up * acceleration * throttle);
        }
        else if (throttle == 0)
        {
            rb.AddForce(-rb.velocity.normalized * frictionForce);
        }
        else if (throttle < 0)
        {
            if (MovingForward())
            {
                rb.AddForce(-rb.velocity.normalized * brakeForce);
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
        return Vector2.Dot(rb.velocity, transform.up) > 0;
    }

    private void Animate()
    {
        Direction currentDirection;

        float result = Vector3.SignedAngle(Vector3.up, transform.up, Vector3.forward);
        angle = result;
        if (-VerticalAmplitude < angle && angle < VerticalAmplitude)
        {
            currentDirection = Direction.Up;
        }
        else if (VerticalAmplitude <= angle && angle <= (180 - VerticalAmplitude))
        {
            currentDirection = Direction.Left;
        }
        else if (-(180 - VerticalAmplitude) <= angle && angle <= -VerticalAmplitude)
        {
            currentDirection = Direction.Right;
        }
        else
        {
            currentDirection = Direction.Down;
        }

        if (previousDirection != currentDirection)
        {
            SetAnimation(previousDirection, false);
            SetAnimation(currentDirection, true);
            previousDirection = currentDirection;
        }
    }

    private void SetAnimation(Direction direction, bool value)
    {
        switch (direction)
        {
            case Direction.Up:
                animator.SetBool("up", value);
                break;
            case Direction.Down:
                animator.SetBool("down", value);
                break;
            case Direction.Left:
                animator.SetBool("left", value);
                break;
            case Direction.Right:
                animator.SetBool("right", value);
                break;
        }
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
