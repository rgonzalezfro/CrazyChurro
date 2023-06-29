using Pathfinding;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
public class NpcAiController : MonoBehaviour
{
    [SerializeField]
    private float navDetectionRadius = 20f;

    [SerializeField]
    private float remainingDistanceToRecalculate = 1f;

    [SerializeField]
    private float navigationCheckInterval = 1f;

    private AIPath agent;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    [Tooltip("El angulo necesario para cambiar de sprite vertical a sprite de lado")]
    [Range(10, 45)]
    public float VerticalAmplitude = 30;

    private float angle;
    private Direction previousDirection;

    private void Start()
    {
        agent = GetComponent<AIPath>();
        StartCoroutine(CheckDestination());

        previousDirection = Direction.Up;
        SetAnimation(previousDirection, true);
    }

    private void OnDestroy()
    {
        StopCoroutine(CheckDestination());
    }

    private void Update()
    {
        Animate();
    }

    private IEnumerator CheckDestination()
    {
        while (true)
        {
            if (!agent.hasPath || agent.reachedDestination || agent.remainingDistance <= remainingDistanceToRecalculate)
            {
                Debug.Log("LOOKING FOR DESTINATION");
                var destination = GetRandomNavPoint();
                agent.destination = destination;
            }

            yield return new WaitForSeconds(navigationCheckInterval);
        }
    }
    
    private Vector3 GetRandomNavPoint()
    {
        var mask = 1 << LayerMask.NameToLayer("Navigation");
        var colliders = Physics2D.OverlapCircleAll(transform.position, navDetectionRadius, mask);

        if (colliders.Length == 0)
        {
            Debug.LogWarning($"{name}: No navigation target found");
            return transform.position;
        }
        var index = Random.Range(0, colliders.Length);

        Debug.Log("DESTINATION found");
        return colliders[index].transform.position;
    }

    private void Animate()
    {
        Direction currentDirection;

        float result = Vector3.SignedAngle(Vector3.up, agent.desiredVelocity, Vector3.forward);
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
