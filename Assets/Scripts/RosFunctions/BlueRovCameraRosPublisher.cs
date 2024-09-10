using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.BuiltinInterfaces;
using RosMessageTypes.Sensor; // ImageMsg
using RosMessageTypes.Std;
using Unity.Robotics.Core;
using System;

// [RequireComponent(typeof(ROSClockSubscriber))]
public class BlueRovCameraRosPublisher : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "blue_rov1/CompressedImage";
    public CameraCapturer cameraCapturer; // i think this will be like Game Object in Unity Tutorial
    public float publishMessageFrequency = 0.5f; // 0.5f
    private float timeElapsed;
    private byte[] image;
    private CameraInfoMsg infoCamera;
    private CompressedImageMsg image_msg;

    private HeaderMsg header;
    private const int isBigEndian = 0;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        this.cameraCapturer = GetComponent<CameraCapturer>();
        ros.RegisterPublisher<CompressedImageMsg>(topicName);
        Debug.Log("Publish Successfully");

        header = new HeaderMsg();

        image_msg = new CompressedImageMsg();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {
            
            image_msg.header = header;
            image = cameraCapturer.getCapturedJpegImage();
            image_msg.data = image;
            ros.Publish(topicName, image_msg);
            timeElapsed = 0;
        }
    }
}