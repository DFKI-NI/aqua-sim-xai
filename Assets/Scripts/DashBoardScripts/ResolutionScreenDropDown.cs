using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionScreenDropDown : MonoBehaviour
{
    private Resolution[] supportedResolutions;

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            SetResolution(1920, 1080);
            Debug.Log("Resolution set to: 1920x1080");
        }
        else if (val == 1)
        {
            SetResolution(1280, 720);
            Debug.Log("Resolution set to: 1280x720");
        }
        else if (val == 2)
        {
            SetResolution(800, 600);
            Debug.Log("Resolution set to: 800x600");
        }
        else if (val == 3)
        {
            SetResolution(640, 480);
            Debug.Log("Resolution set to: 640x480");
        }
    }

    private void SetResolution(int width, int height) { Screen.SetResolution(width, height, true); }
}
