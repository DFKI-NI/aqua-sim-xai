using UnityEngine;
using UnityEngine.AI;

// TODO: This is currently work for the only active boat (berky or otter) in the scene.
public class ControlModes : MonoBehaviour
{
    private GameObject[] robotBoats;

    private void Start()
    {
        robotBoats = GameObject.FindGameObjectsWithTag("robot");
        Debug.Log("Number of robot boats: " + robotBoats.Length);
        // debug names of robot boats
        foreach (GameObject robotBoat in robotBoats)
        {
            Debug.Log(robotBoat.name);
        }
        SetManualMode();
    }

    private void TogglePatrolling(bool enable)
    {
        foreach (GameObject robotBoat in robotBoats)
        {
            // Toggle PatrollingPolice script
            PatrollingPolice patrollingPolice = robotBoat.GetComponent<PatrollingPolice>();
            if (patrollingPolice != null) patrollingPolice.enabled = enable;
        }
    }

    private void ToggleNavMeshAgent(bool enable)
    {
        foreach (GameObject robotBoat in robotBoats)
        {
            // Toggle NavMeshAgent component
            NavMeshAgent navMeshAgent = robotBoat.GetComponent<NavMeshAgent>();
            if (navMeshAgent != null) navMeshAgent.enabled = enable;
        }
    }

    private void ToggleNavMeshObstacle(bool enable)
    {
        foreach (GameObject robotBoat in robotBoats)
        {
            // Toggle NavMeshObstacle component
            NavMeshObstacle navMeshObstacle = robotBoat.GetComponent<NavMeshObstacle>();
            if (navMeshObstacle != null) navMeshObstacle.enabled = enable;
        }
    }

    public void SetManualMode()
    {
        Debug.Log("Manual Mode");
        TogglePatrolling(false);
        ToggleNavMeshAgent(false);
        ToggleNavMeshObstacle(true);
    }

    public void SetTrajVizMode()
    {
        Debug.Log("Trajectory Visualization Mode");
        foreach (GameObject robotBoat in robotBoats)
        {
            TrajViz visualizer = robotBoat.GetComponent<TrajViz>();
            if (visualizer != null) visualizer.enabled = true;
        }
    }

    public void SetAutoMode()
    {
        Debug.Log("Auto Mode");
        ToggleNavMeshObstacle(false);
        ToggleNavMeshAgent(true);
        TogglePatrolling(true);
    }
}
