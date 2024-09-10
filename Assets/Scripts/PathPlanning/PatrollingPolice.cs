using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// PatrollingPolice are responsible for patrolling a given area
/// </summary>
public class PatrollingPolice : MonoBehaviour
{
    public RandomPointGenerator randomPointGenerator;
    [SerializeField]
    private BoxCollider areaOfPatrol;
    [SerializeField]
    private float minVelocity = 2.0f;
    [SerializeField]
    private float maxVelocity = 15.0f;
    [SerializeField]
    private float velocityCheckDuration = 2.0f;
    [SerializeField]
    private float velocityDecreaseThreshold = 0.1f;

    private NavMeshAgent navMeshAgent;
    private Vector3[] patrollingRandomPoints;
    private int currentPatrolIndex = 0;
    private float velocityCheckTimer = 0.0f;
    private bool isVelocityDecreased = false;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.autoBraking = false;
        SetSpeed();
        randomPointGenerator = FindObjectOfType<RandomPointGenerator>();
        patrollingRandomPoints = randomPointGenerator.GenerateRandomPoints();
    }

    void Update()
    {
        PatrolRandomPoints();
        CheckVelocityDecrease();
    }

    void PatrolRandomPoints()
    {
        if (patrollingRandomPoints.Length == 0)
        {
            Debug.LogError("No random points generated!");
            return;
        }

        if (currentPatrolIndex < patrollingRandomPoints.Length)
        {
            navMeshAgent.SetDestination(patrollingRandomPoints[currentPatrolIndex]);
            if ((Vector3.Distance(transform.position, patrollingRandomPoints[currentPatrolIndex]) <
                 1.0f) ||
                isVelocityDecreased)
            {
                currentPatrolIndex++;
            }
        }
        else
        {
            currentPatrolIndex = 0;
        }
    }

    void SetSpeed() { navMeshAgent.speed = Random.Range(minVelocity, maxVelocity); }

    void CheckVelocityDecrease()
    {
        if (navMeshAgent.velocity.magnitude < velocityDecreaseThreshold)
        {
            velocityCheckTimer += Time.deltaTime;
            if (velocityCheckTimer >= velocityCheckDuration)
            {
                isVelocityDecreased = true;
                // Debug.Log("Velocity decreased for some time.");
            }
        }
        else
        {
            velocityCheckTimer = 0.0f;
            isVelocityDecreased = false;
        }
    }
}
