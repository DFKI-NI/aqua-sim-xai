using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionScreen : MonoBehaviour
{
    private Resolution[] supportedResolutions;

    private void Start() { supportedResolutions = Screen.resolutions; }

    public void SetResolution1920x1080() { SetResolution(1920, 1080); }

    public void SetResolution1280x720() { SetResolution(1280, 720); }

    private void SetResolution(int width, int height) { Screen.SetResolution(width, height, true); }
}
