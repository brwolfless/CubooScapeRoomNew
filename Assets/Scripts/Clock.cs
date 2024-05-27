using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    void Update()
    {
        DateTime time = DateTime.Now;

        float hours = time.Hour % 12;
        float minutes = time.Minute;
        float seconds = time.Second;

        // Calculando a rotação dos ponteiros
        float hourRotation = hours * 30f + (minutes / 2f); // 360° dividido em 12 horas
        float minuteRotation = minutes * 6f; // 360° dividido em 60 minutos
        float secondRotation = seconds * 6f; // 360° dividido em 60 segundos

        // Aplicando as rotações
        hourHand.rotation = Quaternion.Euler(0f, 90f, hourRotation);
        minuteHand.rotation = Quaternion.Euler(0f, 90f, minuteRotation);
        secondHand.rotation = Quaternion.Euler(0f, 90f, secondRotation);
    }
}