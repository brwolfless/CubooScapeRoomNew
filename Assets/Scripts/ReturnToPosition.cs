using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPosition : MonoBehaviour
{
    public Transform targetObject; // Objeto cuja posição será usada como referência
    public float returnSpeed = 5f; // Velocidade de retorno do objeto

    private Vector3 originalPosition; // Posição original do objeto

    void Start()
    {
        originalPosition = transform.position; // Guarda a posição original do objeto
    }

    void OnCollisionEnter(Collision collision)
    {
        // Se o objeto colidir com o chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Chama a função para iniciar o movimento de retorno
            StartCoroutine(ReturnToObject());
        }
    }

    IEnumerator ReturnToObject()
    {
        float journeyLength = Vector3.Distance(transform.position, targetObject.position);
        float startTime = Time.time;

        while (true)
        {
            float distanceCovered = (Time.time - startTime) * returnSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, targetObject.position, fractionOfJourney);

            if (fractionOfJourney >= 1f)
            {
                break;
            }

            yield return null;
        }
    }
}
