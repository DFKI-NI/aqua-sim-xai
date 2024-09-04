using System;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

public class ROSMainMenuSettings : MonoBehaviour
{
    private string ROSIP = "192.168.178.25";
    private string ROSPort = "10000";

    public ReadInputIP m_ReadInput;
    private string m_PreviousROSIP;
    private string m_PreviousROSPort;
    private ROSConnection m_ROS;
    private void Start()
    {
        m_PreviousROSIP = ROSIP;
        m_PreviousROSPort = ROSPort;

        m_ROS = ROSConnection.GetOrCreateInstance();
        m_ReadInput = GetComponent<ReadInputIP>();
        UpdateROSConnectionSettings();
    }

    private void FixedUpdate() { UpdateROSConnectionSettings(); }

    private void UpdateROSConnectionSettings()
    {
        if (m_ReadInput != null)
        {
            string inputString = m_ReadInput.GetInputString();
            string[] input = inputString.Split(':');
            string ip = input[0].Trim();
            string port = input[1].Trim();

            if (IsValidIPAddress(ip) && IsValidPort(port))
            {
                ROSIP = ip;
                ROSPort = port;
                m_ROS.RosIPAddress = ROSIP;
                m_ROS.RosPort = Int32.Parse(ROSPort);
            }
            else
            {
                Debug.LogError("Invalid ROS IP or Port settings.");
            }
        }
        else
        {
            Debug.LogError("ReadInput script not found.");
        }

#region Restart ROS Connection

        if (ROSIP != m_PreviousROSIP || ROSPort != m_PreviousROSPort)
        {
            RestartROSConnection();
            m_PreviousROSIP = ROSIP;
            m_PreviousROSPort = ROSPort;
        }

#endregion
    }

    private bool IsValidIPAddress(string ipAddress)
    {
        string[] octets = ipAddress.Split('.');
        if (octets.Length != 4) return false;
        foreach (string octet in octets)
        {
            if (!byte.TryParse(octet, out _)) return false;
        }
        return true;
    }

    private bool IsValidPort(string port)
    {
        return Int32.TryParse(port, out int portNumber) && portNumber >= 0 && portNumber <= 65535;
    }

    public void RestartROSConnection()
    {
        // Disconnect current ROS connection
        m_ROS.Disconnect();

        // Update ROS IP and Port settings
        m_ROS.RosIPAddress = ROSIP;
        m_ROS.RosPort = Int32.Parse(ROSPort);

        // Reconnect with new settings
        m_ROS.Connect();

        Debug.Log("ROS connection restarted.");
    }
}