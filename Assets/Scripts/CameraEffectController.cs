using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]

public class CameraEffectController : MonoBehaviour
{
    [SerializeField] private Shader shader;
    
    private Material material;

    private void OnEnable()
    {
        if (shader && shader.isSupported)
        {
            material = new Material(shader);
        }
        else
        {
            enabled = false;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material)
        {
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}