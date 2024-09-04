using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField]
    private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private float minVelocity = 2.0f;
    [SerializeField]
    private float maxVelocity = 15.0f;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetSpeed();
    }

    void Update() { navMeshAgent.SetDestination(movePositionTransform.position); }

    void SetSpeed() { navMeshAgent.speed = Random.Range(minVelocity, maxVelocity); }
}