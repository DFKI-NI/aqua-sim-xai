using System;
using System.Collections.Generic;
using RosMessageTypes.Geometry;                   // PoseArray message class
using Unity.Robotics.Visualizations;              // Visualizations
using Unity.Robotics.ROSTCPConnector;             // ROS Connector
using Unity.Robotics.ROSTCPConnector.ROSGeometry; // Coordinate space utilities
using UnityEngine;

// public class WaypointPublisher : HistoryDrawingVisualizer<PoseArrayMsg>
// {
//     [SerializeField]
//     Color m_Color = Color.white;
//     [SerializeField]
//     float m_Thickness = 0.1f;
//     [SerializeField]
//     string m_Label = "";

//     public override Action CreateGUI(IEnumerable<Tuple<PoseArrayMsg,
//     MessageMetadata>> messages)
//     {
//         return () =>
//         {
//             var count = 0;
//             foreach (var (message, meta) in messages)
//             {
//                 GUILayout.Label($"Waypoint Array #{count}:");

//                 // Display the relevant information from the PoseArray
//                 message foreach (var pose in message.poses)
//                 {
//                     GUILayout.Label($"Position: {pose.position}");
//                     GUILayout.Label($"Orientation: {pose.orientation}");
//                 }

//                 count++;
//             }
//         };
//     }

//     public override void Draw(Drawing3d drawing,
//     IEnumerable<Tuple<PoseArrayMsg, MessageMetadata>> messages)
//     {
//         var firstPass = true;
//         var prevPoint = Vector3.zero;
//         var color = Color.white;
//         var label = "";

//         Debug.Log("Drawing waypoints");

//         foreach (var (msg, meta) in messages)
//         {
//             foreach (var pose in msg.poses)
//             {
//                 var point = pose.position.From<FLU>();

//                 if (firstPass)
//                 {
//                     color = VisualizationUtils.SelectColor(m_Color, meta);
//                     label = VisualizationUtils.SelectLabel(m_Label, meta);
//                     firstPass = false;
//                 }
//                 else
//                 {
//                     drawing.DrawLine(prevPoint, point, color, m_Thickness);
//                 }

//                 prevPoint = point;
//             }
//         }

//         drawing.DrawLabel(label, prevPoint, color);
//     }
// }

public class WaypointPublisher : HistoryDrawingVisualizer<PoseArrayMsg>
{
    [SerializeField]
    Color m_Color = Color.white;
    [SerializeField]
    float m_Thickness = 0.1f;
    [SerializeField]
    string m_Label = "";

    public override Action CreateGUI(IEnumerable<Tuple<PoseArrayMsg, MessageMetadata>> messages)
    {
        return () =>
        {
            var count = 0;
            foreach (var (message, meta) in messages)
            {
                GUILayout.Label($"Waypoint Array #{count}:");

                // Display the relevant information from the PoseArray message
                foreach (var pose in message.poses)
                {
                    GUILayout.Label($"Position: {pose.position}");
                    GUILayout.Label($"Orientation: {pose.orientation}");
                }

                count++;
            }
        };
    }

    public override void Draw(Drawing3d drawing,
                              IEnumerable<Tuple<PoseArrayMsg, MessageMetadata>> messages)
    {
        var firstPass = true;
        var prevPoint = Vector3.zero;
        var color = Color.white;
        var label = "";

        Debug.Log("Drawing waypoints");

        foreach (var (msg, meta) in messages)
        {
            foreach (var pose in msg.poses)
            {
                var position = pose.position.From<FLU>();

                // FIXME: Apply rotation around the y-axis by 270 degrees
                var rotatedPosition = Quaternion.Euler(0, 270, 0) * position;

                // FIXME: Translate in the y-direction by 142
                rotatedPosition.y += 142;

                if (firstPass)
                {
                    color = VisualizationUtils.SelectColor(m_Color, meta);
                    label = VisualizationUtils.SelectLabel(m_Label, meta);
                    firstPass = false;
                }
                else
                {
                    drawing.DrawLine(prevPoint, rotatedPosition, color, m_Thickness);
                }

                prevPoint = rotatedPosition;
            }
        }

        drawing.DrawLabel(label, prevPoint, color);
    }
}
