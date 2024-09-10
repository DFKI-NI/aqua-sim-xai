using UnityEngine;

public class SphereCollisionHandler : MonoBehaviour
{
    private bool collidedWithRobot = false;
    private Material greenMaterial;

    public void SetGreenMaterial(Material material) { greenMaterial = material; }

    public bool HasCollidedWithRobot() { return collidedWithRobot; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("robot"))
        {
            Debug.Log("Sphere collided with the robot");
            collidedWithRobot = true;
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null && greenMaterial != null)
            {
                renderer.material = greenMaterial;
            }
            else
            {
                Debug.LogError("Renderer component or greenMaterial not found");
            }
        }
    }
}
