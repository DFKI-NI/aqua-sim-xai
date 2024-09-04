using UnityEngine;

/// <summary>
/// Visualizes the trajectory of the robot/boat in the scene to until the UGS
/// UGS: User Guidance System
/// This system for having a random point generator and visualizing the trajectory to help the user
/// navigate
/// </summary>

public class TrajViz : MonoBehaviour
{
    [SerializeField]
    private Transform WayPointsGenerator;

    private GameObject robot;
    private GameObject startSphere;
    private Vector3 robotPos;
    private Vector3 startSpherePos;

    void Start()
    {
        robot = GameObject.FindGameObjectWithTag("robot");

        if (WayPointsGenerator != null)
        {
            startSphere = WayPointsGenerator.GetChild(0).gameObject;
            Debug.Log("StartSphere: " + startSphere);

            if (startSphere != null) startSpherePos = startSphere.transform.position;
        }
        else
        {
            Debug.LogWarning("Sphere parent is not assigned. Cannot find startSphere.");
        }

        if (robot != null) robotPos = robot.transform.position;
    }

    void FixedUpdate()
    {
        if (robot != null && startSphere != null)
        {
            robotPos = robot.transform.position;
            startSpherePos = startSphere.transform.position;
            Debug.DrawLine(robotPos, startSpherePos, Color.yellow, 0.1f);
        }
        else
        {
            // Debug.LogWarning("Either robot or startSphere is null. Cannot draw line.");
        }
    }
}
