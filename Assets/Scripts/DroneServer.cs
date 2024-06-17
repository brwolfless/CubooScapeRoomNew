using Best.SocketIO.Events;
using Server;
using Server.Json.JsonObjects;
using Server.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Management;

public class DroneServer : MonoBehaviour
{ 
    public ObjectController objectController;
    public GameObject Saida;
    public AudioSource notification;

    private void OnEnable()
    {
        ServerEvents.OnReceivedPilot += OnReceivedPilot;
        //Input.gyro.enabled = false;
    }

    private void OnDisable()
    {
        ServerEvents.OnReceivedPilot -= OnReceivedPilot;
    }

    //private void Start()
    //{
    //    Input.gyro.enabled = true;
    //}

    void OnReceivedPilot(ReceivedPiloto pilotao)
    {
        if(pilotao.pilotEvent.eventName == Server.EventNames.BiforstActivated)
        {
            if(pilotao.pilotEvent.message == "escritorio")
            {
                objectController.sceneToGo = "Escritorio";
                Saida.SetActive(true);
                notification.Play();
            }
            else if(pilotao.pilotEvent.message == "praia")
            {
                objectController.sceneToGo = "Praia";
                Saida.SetActive(true);
                notification.Play();
            }
        }
    }
}
