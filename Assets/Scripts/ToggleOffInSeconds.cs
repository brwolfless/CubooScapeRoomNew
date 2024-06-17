using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Server.Scripts;

public class ToggleOffInSeconds : MonoBehaviour
{
    public float time;
    public bool isStart;
    public ToggleOffInSeconds historia;
    public ToggleOffInSeconds controles;

    //private void Start()
    //{
    //    if (isStart)
    //    {
    //        historia.StartTimer();
    //        controles.StartTimerChangeScene("Drone");
    //        gameObject.SetActive(false);
    //    }
    //}

    private void OnEnable()
    {
        ServerEvents.OnGameStarted += StartGame;
    }
    private void OnDisable()
    {
        ServerEvents.OnGameStarted -= StartGame;
    }

    void StartGame()
    {
        if (isStart)
        {
            //historia.StartTimer();
            controles.StartTimerChangeScene("Drone");
            //gameObject.SetActive(false);
        }
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    public void StartTimerChangeScene(string scene)
    {
        StartCoroutine(TimerScene(scene));
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    IEnumerator TimerScene(string scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
}
