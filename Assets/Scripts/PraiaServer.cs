using Best.SocketIO.Events;
using Server;
using Server.Json.JsonObjects;
using Server.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PraiaServer : MonoBehaviour
{
    [SerializeField] GameObject AskingBifrost;
    [SerializeField] GameObject BifrostArrived;
    public AudioSource notification;

    private void OnEnable()
    {
        ServerEvents.OnReceivedPilot += OnReceivedPilot;
    }

    private void OnDisable()
    {
        ServerEvents.OnReceivedPilot -= OnReceivedPilot;
    }

    void OnReceivedPilot(ReceivedPiloto pilotao)
    {
        if (pilotao.pilotEvent.eventName == Server.EventNames.BiforstActivated)
        {
            AskingBifrost.SetActive(false);
            BifrostArrived.SetActive(true);
            notification.Play();
        }
    }
}
