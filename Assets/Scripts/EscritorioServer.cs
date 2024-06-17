using Best.SocketIO.Events;
using Server;
using Server.Json.JsonObjects;
using Server.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EscritorioServer : MonoBehaviour
{
    [SerializeField] GameObject AskingBifrost;
    [SerializeField] GameObject BifrostArrived;
    [SerializeField] GameObject Player;
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
        if (pilotao.pilotEvent.eventName == Server.EventNames.BiforstActivated && Player.transform.position.z < -7.186f)
        {
            AskingBifrost.SetActive(false);
            BifrostArrived.SetActive(true);
        }
    }
}
