using UnityEngine;
using UnityEngine.UI;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class SubmitPromptRos : MonoBehaviour
{
    public InputField explainInputField;
    public InputField actionInputField;
    private string m_PromptTopic = "prompt";
    private ROSConnection m_ROS;

    void Start()
    {
        m_ROS = ROSConnection.GetOrCreateInstance();
        m_ROS.RegisterPublisher<StringMsg>(m_PromptTopic);
    }

    public void SubmitPrompt()
    {
        string prompt = "";

        Debug.Log("Submit prompt button clicked.");
        Debug.Log("Explain input field: " + explainInputField.text);

        if (explainInputField != null && !string.IsNullOrEmpty(explainInputField.text))
        {
            prompt += explainInputField.text;
            Debug.Log("Explain prompt: " + explainInputField.text);
        }

        if (actionInputField != null && !string.IsNullOrEmpty(actionInputField.text))
        {
            if (!string.IsNullOrEmpty(prompt))
            {
                prompt += ", "; // Add comma as separator if explain prompt is not empty
            }
            prompt += actionInputField.text;
            Debug.Log("Action prompt: " + actionInputField.text);
        }

        if (!string.IsNullOrEmpty(prompt))
        {
            StringMsg promptMsg = new StringMsg();
            promptMsg.data = prompt;
            m_ROS.Send(m_PromptTopic, promptMsg);
            Debug.Log("Prompt message sent: " + prompt);
        }
        else
        {
            Debug.Log("No prompts to send.");
        }
    }
}
