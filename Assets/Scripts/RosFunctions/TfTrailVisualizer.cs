using System;
using System.Collections.Generic;
using RosMessageTypes.Geometry;                   // Generated message classes
using Unity.Robotics.Visualizations;              // Visualizations
using Unity.Robotics.ROSTCPConnector;             // ROS Connector
using Unity.Robotics.ROSTCPConnector.ROSGeometry; // Coordinate space utilities
using RosMessageTypes.Tf2;                        // TF message classes
using UnityEngine;

public class TfTrailVisualizer : HistoryDrawingVisualizer<TFMessageMsg>
{
    [SerializeField]
    Color m_Color = Color.white;
    [SerializeField]
    float m_Thickness = 0.1f;
    [SerializeField]
    string m_Label = "";

    public override Action CreateGUI(IEnumerable<Tuple<TFMessageMsg, MessageMetadata>> messages)
    {
        return () =>
        {
            var count = 0;
            foreach (var (message, meta) in messages)
            {
                GUILayout.Label($"Trail #{count}:");
                foreach (var transform in message.transforms)
                {
                    transform.transform.GUI();
                }
                count++;
            }
        };
    }

    public override void Draw(Drawing3d drawing,
                              IEnumerable<Tuple<TFMessageMsg, MessageMetadata>> messages)
    {
        var firstPass = true;
        var prevPoint = Vector3.zero;
        var color = Color.white;
        var label = "";

        foreach (var (msg, meta) in messages)
        {
            foreach (var transform in msg.transforms)
            {
                var point = transform.transform.translation.From<FLU>();
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
        }

        drawing.DrawLabel(label, prevPoint, color);
    }
}
