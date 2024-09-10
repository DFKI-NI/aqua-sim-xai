using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInputIP : MonoBehaviour
{
    private string inputString = "10.248.4.81:10000";

    public void ReadStringInput(string s)
    {
        inputString = s;
        Debug.Log("IP and Port: " + inputString);
    }

    public string GetInputString() { return inputString; }
}
