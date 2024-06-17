using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Server.Scripts;
using UnityEngine.SceneManagement;

public class TriggerEndgame : MonoBehaviour
{
    public GameObject[] endgamescreens;
    public float timeToEnd;
    public float timeToChangeScene;
    public AudioSource winsfx;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Endgame", timeToEnd);
        Invoke("ChangeScene", timeToChangeScene);
    }

    void Endgame()
    {
        Explorer.ExploradorObject.gameId = WebSocketIOController.Instance.GameInfo.gameId;
        Explorer.ExploradorObject.eventName = Server.EventNames.EndGame;
        Explorer.ExploradorObject.message = "end";
        ServerEvents.OnSendExplorer.Invoke();
        for (int i = 0; i < endgamescreens.Length; i++)
        {
            endgamescreens[i].SetActive(true);
        }
        winsfx.Play();
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("GDTStart");
    }
}
