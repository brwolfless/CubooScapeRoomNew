using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // Referência ao componente Animator do personagem
    

    void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            
            animator.SetBool("Resgatado" , true);
                // Ativa a animação do personagem
           
        }
    }
}