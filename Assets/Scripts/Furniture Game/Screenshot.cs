using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [Header("References")]
    public List<RenderTexture> _renderTextures = new();

    public void TakeLevelScreenshot(int level)
    {
        // Create a new temporary texture with the same dimensions as the screen
        RenderTexture tempRenderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);

        // Render the camera's view to the temporary texture
        Camera.main.targetTexture = tempRenderTexture;
        Camera.main.Render();
        Camera.main.targetTexture = null;

        // Copy the pixels from the temporary texture to the render texture
        Graphics.Blit(tempRenderTexture, _renderTextures[level]);


        // Release the temporary texture
        RenderTexture.ReleaseTemporary(tempRenderTexture);

    }
}