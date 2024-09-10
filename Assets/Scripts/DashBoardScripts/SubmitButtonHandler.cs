using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class SubmitButtonHandler : MonoBehaviour
{
    public ReadInputAction m_InputAction;
    public ReadInputExplain m_InputExplain;

    private ROSConnection m_ROS;
    private string m_FullExplainPromptTopic = "full_explain";

    void Start()
    {
        m_ROS = ROSConnection.GetOrCreateInstance();
        m_ROS.RegisterPublisher<StringMsg>(m_FullExplainPromptTopic);
        Debug.Log("Published Full Explain Prompt topic Successfully");
    }

    public void OnSubmitButtonClicked()
    {

        string actionInput = m_InputAction.GetInputString();
        string explainInput = m_InputExplain.GetInputString();

        // Check if input strings are null or empty
        if (string.IsNullOrEmpty(actionInput) || string.IsNullOrEmpty(explainInput))
        {
            Debug.Log("Please enter both action and explanation");
            return;
        }
        else
        {
            Debug.Log("Action: " + actionInput);
            Debug.Log("Explanation: " + explainInput);

            StringMsg fullExplainPromptMsg = new StringMsg();
            fullExplainPromptMsg.data = actionInput + "," + explainInput;
            m_ROS.Publish(m_FullExplainPromptTopic, fullExplainPromptMsg);
        }
    }
}
