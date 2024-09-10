using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class ExplainPromptRos : MonoBehaviour
{
    public ReadInputExplain m_InputExplain;
    public string m_ExplainPromptTopic = "explain_prompt";
    private ROSConnection m_ROS;
    private string m_PreviousPrompt;

    void Start()
    {
        m_ROS = ROSConnection.GetOrCreateInstance();
        m_ROS.RegisterPublisher<StringMsg>(m_ExplainPromptTopic);
        Debug.Log("Publish Explain Prompt topic Successfully");
        m_InputExplain = GetComponent<ReadInputExplain>();
        m_PreviousPrompt = null;
    }

    void FixedUpdate()
    {
        string currentPrompt = m_InputExplain.GetInputString();
        if (currentPrompt != m_PreviousPrompt && currentPrompt != null)
        {
            StringMsg explainPromptMsg = new StringMsg();
            explainPromptMsg.data = currentPrompt;
            m_ROS.Publish(m_ExplainPromptTopic, explainPromptMsg);
            m_PreviousPrompt = currentPrompt;
        }
    }
}
