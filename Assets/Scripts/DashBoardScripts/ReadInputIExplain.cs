using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInputExplain : MonoBehaviour
{
    private string inputString;

    public void ReadStringInput(string s) { inputString = s; }

    public string GetInputString() { return inputString; }
}
