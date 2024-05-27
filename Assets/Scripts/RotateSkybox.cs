using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Velocidade de rotação

    void Update()
    {
        // Rotaciona o skybox ao longo do eixo Y
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}