using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class ActionPromptRos : MonoBehaviour
{
    public ReadInputAction m_InputAction;
    private ROSConnection m_ROS;
    private string m_ActionPromptTopic = "action_prompt";
    private string m_PreviousPrompt;

    void Start()
    {
        m_ROS = ROSConnection.GetOrCreateInstance();
        m_ROS.RegisterPublisher<StringMsg>(m_ActionPromptTopic);
        Debug.Log("Publish Action Prompt topic Successfully");
        m_InputAction = GetComponent<ReadInputAction>();
        m_PreviousPrompt = null;
    }

    void FixedUpdate()
    {
        string currentPrompt = m_InputAction.GetInputString();
        if (currentPrompt != m_PreviousPrompt && currentPrompt != null)
        {
            StringMsg actionPromptMsg = new StringMsg();
            actionPromptMsg.data = currentPrompt;
            m_ROS.Publish(m_ActionPromptTopic, actionPromptMsg);
            m_PreviousPrompt = currentPrompt;
        }
    }
}
