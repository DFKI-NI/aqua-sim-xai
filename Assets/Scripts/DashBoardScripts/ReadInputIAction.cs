using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInputAction : MonoBehaviour
{
    private string inputString;

    public void ReadStringInput(string s) { inputString = s; }

    public string GetInputString() { return inputString; }
}
