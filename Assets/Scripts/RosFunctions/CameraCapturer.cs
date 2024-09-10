using System;
using UnityEngine;

public class CameraCapturer : MonoBehaviour
{
    public bool IsCaptureEnable = false;
    Camera _camera;
    public int resWidth;
    public int resHeight;
    public byte[] jpg;

    // Store the original culling mask
    int originalCullingMask;

    void Start()
    {
        this._camera = GetComponent<Camera>();
        // Store the original culling mask
        originalCullingMask = _camera.cullingMask;
    }

    private void FixedUpdate()
    {
        if (this.IsCaptureEnable)
        {
            this.jpg = getJPGFromCurrentCamera();
            this.IsCaptureEnable = false;
        }
    }

    public byte[] getCapturedJpegImage()
    {
        this.IsCaptureEnable = true;
        while (this.IsCaptureEnable)
        {
            if (this.jpg != null)
            {
                return this.jpg;
            }
        }
        return null;
    }

    private byte[] getJPGFromCurrentCamera()
    {
        try
        {
            // Store the original culling mask (to ignore the "Ignore Viz" layer)
            originalCullingMask = _camera.cullingMask;

            // Temporarily adjust the culling mask to exclude "Ignore Viz"
            _camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Ignore Viz"));

            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            _camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            _camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            screenShot.Apply();
            _camera.targetTexture = null;
            RenderTexture.active = null;
            byte[] bytes = screenShot.EncodeToJPG();

            // Proper cleanup
            Destroy(rt);
            Destroy(screenShot);

            // Restore the original culling mask
            _camera.cullingMask = originalCullingMask;

            return bytes;
        }
        catch (Exception e)
        {
            Debug.Log("Error");
            Debug.Log(e);
            return null;
        }
    }
}
