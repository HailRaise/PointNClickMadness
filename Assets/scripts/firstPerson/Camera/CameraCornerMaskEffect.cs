using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraCornerMaskEffect : MonoBehaviour
{
    public Material maskMat;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (maskMat != null)
            Graphics.Blit(src, dest, maskMat);
        else
            Graphics.Blit(src, dest);
    }
}
