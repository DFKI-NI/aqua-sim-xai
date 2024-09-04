using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.Core;
using RosMessageTypes.BuiltinInterfaces;
using RosMessageTypes.Sensor; // PCL
using RosMessageTypes.Std;
using System;
using UnityEngine.Serialization;

/*
Script to publish PointCloud2 messages to ROS
Attach this script with component that has raycasters attached to it
*/
public class PointCloudPublisher : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "sonar/PCL";
    public float publishMessageFrequency = 0.5f; // 0.5f
    private float timeElapsed;
    private HeaderMsg header;
    private PointCloud2Msg pointCloud2Msg;
    private PointFieldMsg[] fields;
    private byte[] data;
    private int pointStep;
    private int rowStep;
    private int width;
    private int height;
    private int pointCount;
    private float[] pointData;
    void Start()
    {

        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PointCloud2Msg>(topicName);
        Debug.Log("Publish Successfully");
        header = new HeaderMsg();
        pointCloud2Msg = new PointCloud2Msg();
        pointCloud2Msg.header = header;
        pointCloud2Msg.height = 1;
        pointCloud2Msg.width = 1;
        pointCloud2Msg.fields = new PointFieldMsg[3];
        pointCloud2Msg.fields[0] = new PointFieldMsg();
        pointCloud2Msg.fields[0].name = "x";
        pointCloud2Msg.fields[0].offset = 0;
        pointCloud2Msg.fields[0].datatype = PointFieldMsg.FLOAT32;
        pointCloud2Msg.fields[0].count = 1;
        pointCloud2Msg.fields[1] = new PointFieldMsg();
        pointCloud2Msg.fields[1].name = "y";
        pointCloud2Msg.fields[1].offset = 4;
        pointCloud2Msg.fields[1].datatype = PointFieldMsg.FLOAT32;
        pointCloud2Msg.fields[1].count = 1;
        pointCloud2Msg.fields[2] = new PointFieldMsg();
        pointCloud2Msg.fields[2].name = "z";
        pointCloud2Msg.fields[2].offset = 8;
        pointCloud2Msg.fields[2].datatype = PointFieldMsg.FLOAT32;
        pointCloud2Msg.fields[2].count = 1;
        pointCloud2Msg.is_bigendian = false;
        pointCloud2Msg.point_step = 12;
        pointCloud2Msg.row_step = 12;
        pointCloud2Msg.data = new byte[12];
        pointCloud2Msg.is_dense = true;
        pointData = new float[3];
        pointData[0] = 0.0f;
        pointData[1] = 0.0f;
        pointData[2] = 0.0f;
        data = new byte[12];
        Buffer.BlockCopy(pointData, 0, data, 0, 12);
        pointCloud2Msg.data = data;
        ros.Send(topicName, pointCloud2Msg);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > publishMessageFrequency)
        {
            // Create a new PointCloud2 message
            pointCloud2Msg = new PointCloud2Msg();
            // header = new HeaderMsg();
            // header.Update();
            // pointCloud2Msg.header = header;
            pointCloud2Msg.height = 1;
            pointCloud2Msg.width = 1;
            pointCloud2Msg.fields = new PointFieldMsg[3];
            pointCloud2Msg.fields[0] = new PointFieldMsg();
            pointCloud2Msg.fields[0].name = "x";
            pointCloud2Msg.fields[0].offset = 0;
            pointCloud2Msg.fields[0].datatype = PointFieldMsg.FLOAT32;
            pointCloud2Msg.fields[0].count = 1;
            pointCloud2Msg.fields[1] = new PointFieldMsg();
            pointCloud2Msg.fields[1].name = "y";
            pointCloud2Msg.fields[1].offset = 4;
            pointCloud2Msg.fields[1].datatype = PointFieldMsg.FLOAT32;
            pointCloud2Msg.fields[1].count = 1;
            pointCloud2Msg.fields[2] = new PointFieldMsg();
            pointCloud2Msg.fields[2].name = "z";
            pointCloud2Msg.fields[2].offset = 8;
            pointCloud2Msg.fields[2].datatype = PointFieldMsg.FLOAT32;
            pointCloud2Msg.fields[2].count = 1;
            pointCloud2Msg.is_bigendian = false;
            pointCloud2Msg.point_step = 12;
            pointCloud2Msg.row_step = 12;
            pointCloud2Msg.data = new byte[12];
            pointCloud2Msg.is_dense = true;
            pointData = new float[3];
            pointData[0] = 0.0f;
            pointData[1] = 0.0f;
            pointData[2] = 0.0f;
            data = new byte[12];
            Buffer.BlockCopy(pointData, 0, data, 0, 12);
            pointCloud2Msg.data = data;
            ros.Send(topicName, pointCloud2Msg);
            timeElapsed = 0;
        }
    }
}
