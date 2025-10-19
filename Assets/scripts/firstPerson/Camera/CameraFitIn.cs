using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FitCameraToSprite : MonoBehaviour
{
    public SpriteRenderer background;
    private Camera cam;
    private int lastW, lastH;

    IEnumerator Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;

        // Wait one frame so sprite bounds are valid
        yield return null;

        Fit();
        lastW = Screen.width;
        lastH = Screen.height;
    }

    void Update()
    {
        if (Screen.width != lastW || Screen.height != lastH)
        {
            Fit();
            lastW = Screen.width;
            lastH = Screen.height;
        }
    }

    void Fit()
    {
        if (background == null)
        {
            Debug.LogError("FitCameraToSprite: Background not assigned!");
            return;
        }

        Bounds b = background.bounds;
        float spriteW = b.size.x;
        float spriteH = b.size.y;
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = spriteW / spriteH;

        if (screenRatio >= targetRatio)
        {
            // Wider screen → fit height
            cam.orthographicSize = spriteH / 2f;
        }
        else
        {
            // Taller screen → fit width
            float difference = targetRatio / screenRatio;
            cam.orthographicSize = spriteH / 2f * difference;
        }


        Debug.Log($"[FitCameraToSprite] Fitted for screen {Screen.width}x{Screen.height}, size={cam.orthographicSize}");
    }
}
