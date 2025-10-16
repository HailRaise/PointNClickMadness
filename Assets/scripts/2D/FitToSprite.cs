using UnityEngine;

[ExecuteAlways]
public class AutoFitCamera : MonoBehaviour
{
    public SpriteRenderer backgroundSprite;

    void Update()
    {
        if (!backgroundSprite) return;
        Camera cam = Camera.main;

        float spriteH = backgroundSprite.bounds.size.y;
        float spriteW = backgroundSprite.bounds.size.x;
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = spriteW / spriteH;

        if (screenRatio >= targetRatio)
            cam.orthographicSize = spriteH / 2;
        else
            cam.orthographicSize = spriteH / 2 / targetRatio * screenRatio;
    }
}
