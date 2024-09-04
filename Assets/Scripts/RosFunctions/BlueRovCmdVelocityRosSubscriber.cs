using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

public class BlueRovCmdVelocityRosSubscriber : MonoBehaviour
{

    public string topic = "cmd_vel";
    public BlueRovVelocityControl blueRovVelocityControl;
    void Start()
    {
        this.blueRovVelocityControl = GetComponent<BlueRovVelocityControl>();
        ROSConnection.GetOrCreateInstance().Subscribe<RosMessageTypes.Geometry.TwistMsg>(
            topic, VelocityChange);
    }

    void VelocityChange(RosMessageTypes.Geometry.TwistMsg velocityMessage)
    {
        Debug.Log("" + velocityMessage);
        this.blueRovVelocityControl.moveVelocity(velocityMessage);
    }
}
