using UnityEngine;
using UnityEngine.Perception.GroundTruth;

[RequireComponent(typeof(Camera))]
public class ImageCapturerGT : MonoBehaviour
{
    // Reference to the PerceptionCamera component
    private Camera m_Camera;

    private PerceptionCamera perceptionCamera;

    void Start()
    {
        m_Camera = GetComponent<Camera>();
        // perceptionCamera = GetComponent<PerceptionCamera>();
        // get perception camera from the object this script attached to :
        perceptionCamera = GetComponent<PerceptionCamera>();
    }

    private void Update()
    {
        // Check for button press to capture image
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CaptureImage();
            Debug.Log("Image Captured");
        }

        Debug.Log("Sensor Handle" + perceptionCamera.SensorHandle);
    }

    private void CaptureImage()
    {
        // // Ensure that PerceptionCamera is assigned
        // if (perceptionCamera == null)
        // {
        //     Debug.LogError("PerceptionCamera reference is not set.");
        //     return;
        // }

        // Trigger the image capture
        // perceptionCamera.captureRgbImages = true;
    }
}
