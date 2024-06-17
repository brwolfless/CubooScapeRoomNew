using Server.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ServerGameConection : MonoBehaviour
{
    public GameObject ServerScreen;
    public GameObject GameScreen;
    [SerializeField] Button GameCode;
    [SerializeField] TMP_InputField GameCodeInput;
    [SerializeField] StartSceneController startSceneController;

    void Start()
    {
        startSceneController = GameObject.Find("Server").GetComponent<StartSceneController>();
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        if (WebSocketIOController.Instance.IsOpen())
        {
            ServerScreen.SetActive(false);
        }
        GameCode.onClick.AddListener(ConnectToGame);
        GameCodeInput.onEndEdit.AddListener(InputGameID);
    }

    private void OnEnable()
    {
        ServerEvents.OnGameConnected += TurnOffGameScreen;
        ServerEvents.OnServerConnected += TurnOffServerScreen;
    }
    private void OnDisable()
    {
        ServerEvents.OnGameConnected -= TurnOffGameScreen;
        ServerEvents.OnServerConnected -= TurnOffServerScreen;
    }

    void TurnOffServerScreen()
    {
        ServerScreen.SetActive(false);
    }
    void TurnOffGameScreen()
    {
        GameScreen.SetActive(false);
        StartCoroutine(StartXR());
    }

    private IEnumerator StartXR()
    {
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Intro");
    }

    void InputGameID(string id)
    {
        startSceneController.InputGameID(id);
    }

    void ConnectToGame()
    {
       startSceneController.ConnectToGame();
    }
}
