using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnOvelap : MonoBehaviour
{
    public GameObject objetoParaTornarVisivel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) // Substitua "SeuTag" pela tag do objeto com o qual quer detectar a colisão
        {
            objetoParaTornarVisivel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground")) // Substitua "SeuTag" pela tag do objeto com o qual quer detectar a colisão
        {
            objetoParaTornarVisivel.SetActive(false);
        }
    }
}
