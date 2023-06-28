using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NpcAiController : MonoBehaviour
{
    [SerializeField]
    private float navDetectionRadius = 20f;

    [SerializeField]
    private float remainingDistanceToRecalculate = 1f;

    [SerializeField]
    private float navigationCheckInterval = 1f;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(CheckDestination());
    }

    private void OnDestroy()
    {
        StopCoroutine(CheckDestination());
    }


    private IEnumerator CheckDestination()
    {
        while (true)
        {
            if (agent.isStopped || agent.remainingDistance <= remainingDistanceToRecalculate)
            {
                var destination = GetRandomNavPoint();
                agent.SetDestination(destination);
            }

            yield return new WaitForSeconds(navigationCheckInterval);
        }
    }


    private Vector3 GetRandomNavPoint()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, navDetectionRadius, LayerMask.NameToLayer("Navigation"));

        if (colliders.Length == 0)
        {
            Debug.LogWarning($"{name}: No navigation target found");
            return transform.position;
        }
        var index = Random.Range(0, colliders.Length);

        return colliders[index].transform.position;
    }
}
