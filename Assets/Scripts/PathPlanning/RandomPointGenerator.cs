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

public abstract class PointGeneratorBase : MonoBehaviour
{
    protected Vector3[] GeneratedPoints;
    public abstract Vector3[] GenerateRandomPoints();
    public abstract Vector3[] GetGeneratedPoints();
}

public class RandomPointGenerator : PointGeneratorBase
{
    [SerializeField]
    private BoxCollider areaOfInterest;
    [SerializeField]
    private int numPoints = 10;
    [SerializeField]
    private float height = 142.0f;
    [SerializeField]
    private float min_distance = 20.0f;
    // private Vector3[] GeneratedPoints;

    void Awake() { GeneratedPoints = GenerateRandomPoints(); }
    /// <summary>
    /// Generates random points within the bounds of the BoxCollider
    /// </summary>
    /// <returns>List of random generated points listed by their x values
    /// </returns>
    public override Vector3[] GenerateRandomPoints()
    {

        Bounds bounds = areaOfInterest.bounds;

        List<Vector3> points = new List<Vector3>();

        // Attempt to make it easier for the user:
        Vector3 prevPoint = GetRandomPointInBounds(bounds);
        points.Add(prevPoint);

        while (points.Count < numPoints)
        {
            Vector3 randomPoint = GetRandomPointInBounds(bounds);
            bool isValid = true;
            foreach (Vector3 point in points)
            {
                if (Vector3.Distance(randomPoint, point) < min_distance)
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                points.Add(randomPoint);
            }
        }
        // Sorting by x and z values:
        points.Sort((a, b) => {
            int xComparison = a.x.CompareTo(b.x);
            if (xComparison != 0)
            {
                return xComparison;
            }
            return a.z.CompareTo(b.z);
        });

        Vector3[] pointsArray = points.ToArray();

        return pointsArray;
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), height,
                           Random.Range(bounds.min.z, bounds.max.z));
    }

    public override Vector3[] GetGeneratedPoints() { return GeneratedPoints; }
}
