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
    private float secondsBetweenCalculations = 0;

    [SerializeField]
    private float navigationCheckInterval = 1f;

    [SerializeField]
    private LayerMask navigationLayer;

    [SerializeField]
    private bool hasDirection;

    private AIPath agent;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<AIPath>();
        SetAnimation(false);

        StartCoroutine(CheckDestination());
        if (hasDirection)
        {
            StartCoroutine(CheckDirection());
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(CheckDestination());
        StopCoroutine(CheckDirection());
    }
    private IEnumerator CheckDestination()
    {
        while (true)
        {
            if (!agent.hasPath || agent.reachedDestination || agent.remainingDistance <= remainingDistanceToRecalculate)
            {
                if (secondsBetweenCalculations > 0)
                {
                    SetAnimation(false);
                    yield return new WaitForSeconds(secondsBetweenCalculations);
                }
                //Debug.Log("LOOKING FOR DESTINATION");
                var destination = GetRandomNavPoint();
                agent.destination = destination;
                SetAnimation(true);
            }

            yield return new WaitForSeconds(navigationCheckInterval);
        }
    }

    private IEnumerator CheckDirection()
    {
        while (true)
        {
            if (agent.velocity.magnitude > 0)
            {
                SetDirection(Vector3.Dot(agent.velocity, Vector3.right) > 0);
            }
            else
            {
                SetDirection(false);
            }
            yield return new WaitForSeconds(navigationCheckInterval);
        }
    }

    private Vector3 GetRandomNavPoint()
    {
        var mask = 1 << LayerMask.NameToLayer("Navigation");
        var colliders = Physics2D.OverlapCircleAll(transform.position, navDetectionRadius, navigationLayer);

        if (colliders.Length == 0)
        {
            Debug.LogWarning($"{name}: No navigation target found");
            return transform.position;
        }
        var index = Random.Range(0, colliders.Length);

        //Debug.Log("DESTINATION found");
        return colliders[index].transform.position;
    }

    private void SetAnimation(bool isMoving)
    {
        animator.SetBool("moving", isMoving);
    }

    private void SetDirection(bool facingRight)
    {
        animator.SetBool("right", facingRight);
    }
}
