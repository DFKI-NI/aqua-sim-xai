using System;
using System.Collections.Generic;
using RosMessageTypes.Nav;                        // Odometry message class
using Unity.Robotics.Visualizations;              // Visualizations
using Unity.Robotics.ROSTCPConnector;             // ROS Connector
using Unity.Robotics.ROSTCPConnector.ROSGeometry; // Coordinate space utilities
using UnityEngine;

public class OdomTrailVisualizer : HistoryDrawingVisualizer<OdometryMsg>
{
    [SerializeField]
    Color m_Color = Color.white;
    [SerializeField]
    float m_Thickness = 0.1f;
    [SerializeField]
    string m_Label = "";

    public override Action CreateGUI(IEnumerable<Tuple<OdometryMsg, MessageMetadata>> messages)
    {
        return () =>
        {
            var count = 0;
            foreach (var (message, meta) in messages)
            {
                GUILayout.Label($"Trail #{count}:");

                // Display the relevant information from the Odometry message
                GUILayout.Label($"Position: {message.pose.pose.position}");
                GUILayout.Label($"Orientation: {message.pose.pose.orientation}");
                GUILayout.Label($"Linear Velocity: {message.twist.twist.linear}");
                GUILayout.Label($"Angular Velocity: {message.twist.twist.angular}");

                count++;
            }
        };
    }

    public override void Draw(Drawing3d drawing,
                              IEnumerable<Tuple<OdometryMsg, MessageMetadata>> messages)
    {
        var firstPass = true;
        var prevPoint = Vector3.zero;
        var color = Color.white;
        var label = "";

        foreach (var (msg, meta) in messages)
        {
            var point = msg.pose.pose.position.From<FLU>();

            if (firstPass)
            {
                color = VisualizationUtils.SelectColor(m_Color, meta);
                label = VisualizationUtils.SelectLabel(m_Label, meta);
                firstPass = false;
            }
            else
            {
                drawing.DrawLine(prevPoint, point, color, m_Thickness);
            }

            prevPoint = point;
        }

        drawing.DrawLabel(label, prevPoint, color);
    }
}
