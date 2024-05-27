using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Material collisionMaterial; // Material a ser aplicado na colisão
    private Material originalMaterial; // Material original do objeto alvo
    private Renderer targetRenderer; // Renderer do objeto alvo


    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto colidido está na camada "reader"
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            // Obtém o Renderer do objeto colidido
            targetRenderer = other.gameObject.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                // Salva o material original
                originalMaterial = targetRenderer.material;
                // Aplica o material de colisão
                targetRenderer.material = collisionMaterial;


            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica se o objeto colidido está na camada "reader"
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            // Verifica se o Renderer foi obtido anteriormente
            if (targetRenderer != null)
            {
                // Restaura o material original
                targetRenderer.material = originalMaterial;
                // Reseta o targetRenderer para null para evitar futuras referências erradas
                targetRenderer = null;

            }
        }
    }
}
