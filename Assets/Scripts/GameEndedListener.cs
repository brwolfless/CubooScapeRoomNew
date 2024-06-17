using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Server.Scripts;
using UnityEngine.SceneManagement;

public class GameEndedListener : MonoBehaviour
{
    private void OnEnable()
    {
        ServerEvents.OnGameEnded += endgame;
    }

    private void OnDisable()
    {
        ServerEvents.OnGameEnded -= endgame;
    }

    void endgame()
    {
        SceneManager.LoadScene("ForceEndgame");
    }
}
