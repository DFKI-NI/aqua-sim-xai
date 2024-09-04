/*Generate random points within a box collider, and generate spheres game
objects These spheres are generated in a pair, and a line is connected between
them to guide the user to follow the trajectory

//Algorithm:
// 1. Generate random points within the bounds of the BoxCollider
// 2. Create first sphere at the first point (Start)
// 3. Check if the sphere is triggered, then generate the next sphere and a line
connecting the two spheres
// 4. Repeat step 3 until all spheres are generated

*/

using UnityEngine;
using System.Collections.Generic;

public class RandomPointVisualizer : MonoBehaviour
{
    [Header("Sphere Renderer")]
    [SerializeField]
    private Material redMaterial;
    [SerializeField]
    private Material greenMaterial;
    [SerializeField]
    private float min_distance = 20.0f;
    [SerializeField]
    private float sphereColliderRadius = 3.0f;

    [Header("Line Renderer")]
    [SerializeField]
    private Material lineMaterial;
    [SerializeField]
    private float lineWidth = 0.5f;

    private LineRenderer[] lineRenderers;
    private GameObject[] spheres;
    private int currentSphereIndex = 0;
    private LineRenderer currentLineRenderer;
    public RandomPointGenerator rPG;
    private int sphereCount = 0;

    void Start()
    {

        rPG = GetComponent<RandomPointGenerator>();
        spheres = new GameObject[rPG.GetGeneratedPoints().Length];
        if (rPG.transform.tag == "WayPointGenerator" && currentSphereIndex == 0)
        {
            CreateSphere(rPG.GetGeneratedPoints()[currentSphereIndex], "StartUGS");
        }
        else
        {
            CreateSphere(rPG.GetGeneratedPoints()[currentSphereIndex]);
        }
        currentSphereIndex = 0;
    }

    void FixedUpdate()
    {
        if (currentSphereIndex < (rPG.GetGeneratedPoints().Length - 1))
        {
            // Check if the sphere has collided with the robot
            if (spheres[currentSphereIndex]
                    .GetComponent<SphereCollisionHandler>()
                    .HasCollidedWithRobot())
            {
                currentSphereIndex++;
                CreateSphere(rPG.GetGeneratedPoints()[currentSphereIndex]);
                Debug.Log("Current Sphere Index: " + currentSphereIndex);

                // GenerateTrajectory between the current and next sphere
                GenerateTrajectory(rPG.GetGeneratedPoints()[currentSphereIndex - 1],
                                   rPG.GetGeneratedPoints()[currentSphereIndex]);
            }
        }
    }
    // TODO: Fix the sphere position w.r.t the parent object    private void CreateSphere(Vector3
    // point, string tagName = "UGS")
    private void CreateSphere(Vector3 point, string tagName = "UGS")
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        marker.layer = LayerMask.NameToLayer("Ignore Viz");

        marker.transform.SetParent(transform);
        marker.transform.position = point;
        Debug.Log("Sphere position: " + point);
        SphereCollider sc = marker.GetComponent<SphereCollider>();
        sc.radius = sphereColliderRadius;
        sc.isTrigger = true;
        Renderer renderer = marker.GetComponent<Renderer>();
        renderer.material = redMaterial;
        SphereCollisionHandler collisionHandler = marker.AddComponent<SphereCollisionHandler>();
        collisionHandler.SetGreenMaterial(greenMaterial);
        marker.tag = tagName;
        spheres[currentSphereIndex] = marker;
    }

    private void GenerateTrajectory(Vector3 firstPoint, Vector3 secondPoint)
    {
        // Destroy the previous line renderer
        if (currentLineRenderer != null)
        {
            Destroy(currentLineRenderer.gameObject);
        }

        // Between 2 points, render a line:
        GameObject lineObject = new GameObject("Line");
        lineObject.transform.SetParent(transform);
        lineObject.layer = LayerMask.NameToLayer("Ignore Viz");

        currentLineRenderer = lineObject.AddComponent<LineRenderer>();
        currentLineRenderer.material = lineMaterial;
        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;

        currentLineRenderer.positionCount = 2;
        currentLineRenderer.SetPosition(0, firstPoint);
        currentLineRenderer.SetPosition(1, secondPoint);
    }
}
