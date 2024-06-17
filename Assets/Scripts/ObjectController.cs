using Best.HTTP.SecureProtocol.Org.BouncyCastle.Bcpg;
using Server.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectController : MonoBehaviour
{
    private const float _minObjectDistance = 2.5f;
    private const float _maxObjectDistance = 3.5f;
    private const float _minObjectHeight = 0.5f;
    private const float _maxObjectHeight = 3.5f;

    private Renderer _myRenderer;

    bool isLooking;
    float timeLookedAt;

    public SliderController slider;
    public enum Funcao
    {
        Teleporte,
        LigarObj,
        Teclar,
        TrocarCena,
        AcharDiscurso,
        CallBifrostEscritorio,
        CallBifrostPraia,
        Animacao,
        EndGame
    }
    public Funcao Acao;
    public Transform teleportLocation;
    public Transform Player;
    public GameObject objToTurnOn;
    public int keyToPress;
    public Keypad keypad;
    public bool isEnd;
    public GameObject[] pontos;
    public GameObject ChamarDrone;
    public string sceneToGo;
    public GameObject thisCanvas;
    public GameObject endCanvas;
    public Animator endAnim;
    public AudioSource sfx;
    //public bool testBool;

    public void Start()
    {
        _myRenderer = GetComponent<Renderer>();
    }

    //private void OnValidate()
    //{
    //    if (testBool)
    //    {
    //        if (Acao == Funcao.EndGame)
    //        {
    //            timeLookedAt = 0;
    //            slider.sliderValue = 0;
    //            GetComponent<BoxCollider>().enabled = false;
    //            endCanvas.SetActive(true);
    //            Invoke("Endgame", 2f);
    //        }
    //        testBool = false;
    //    }
    //}

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) > 0.1f)
        {
            if (Acao == Funcao.TrocarCena)
            {
                if(Vector3.Distance(Player.transform.position, transform.position) < 55)
                    GetComponent<SphereCollider>().enabled = true;
                else
                    GetComponent<SphereCollider>().enabled = false;
            }

            if (thisCanvas != null && !thisCanvas.activeSelf)
            {
                thisCanvas.SetActive(true);
            }

            if (isLooking)
            {
                timeLookedAt += Time.deltaTime;
                slider.sliderValue += Time.deltaTime;

                if (timeLookedAt >= 2)
                {
                    if (Acao == Funcao.Teleporte)
                    {
                        Player.transform.position = teleportLocation.position;
                        timeLookedAt = 0;
                        slider.sliderValue = 0;
                        sfx.Play();
                        if (isEnd)
                        {
                            for (int i = 0; i < pontos.Length; i++)
                            {
                                pontos[i].SetActive(false);
                            }
                            ChamarDrone.SetActive(true);
                        }
                    }
                    else if (Acao == Funcao.LigarObj)
                    {
                        timeLookedAt = 0;
                        slider.sliderValue = 0;
                        objToTurnOn.SetActive(true);
                        sfx.Play();
                    }
                    else if (Acao == Funcao.Teclar)
                    {
                        keypad.AddKey(keyToPress.ToString());
                        timeLookedAt = 0;
                        slider.sliderValue = 0;
                    }
                    else if (Acao == Funcao.TrocarCena)
                    {
                        timeLookedAt = 0;
                        slider.sliderValue = 0;
                        
                        Explorer.ExploradorObject.gameId = WebSocketIOController.Instance.GameInfo.gameId;
                        if (sceneToGo == "Drone")
                        {
                            Explorer.ExploradorObject.eventName = Server.EventNames.EnterDrone;
                            Explorer.ExploradorObject.message = SceneManager.GetActiveScene().name.ToLower();
                        }
                        else 
                        {
                            Explorer.ExploradorObject.eventName = Server.EventNames.ExitDrone;
                            Explorer.ExploradorObject.message = sceneToGo.ToLower();
                        }
                        ServerEvents.OnSendExplorer.Invoke();

                        sfx.Play();
                        Invoke("changeScene", 0.15f);
                        //UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToGo);
                    }
                    else if (Acao == Funcao.AcharDiscurso)
                    {
                        timeLookedAt = 0;
                        slider.sliderValue = 0;
                        Explorer.ExploradorObject.gameId = WebSocketIOController.Instance.GameInfo.gameId;
                        Explorer.ExploradorObject.eventName = Server.EventNames.DocumentDownloaded;
                        Explorer.ExploradorObject.message = "3";
                        ServerEvents.OnSendExplorer.Invoke();
                        sfx.Play();
                    }
                    else if (Acao == Funcao.CallBifrostEscritorio)
                    {
                        timeLookedAt = 0;
                        slider.sliderValue = 0;
                        Explorer.ExploradorObject.gameId = WebSocketIOController.Instance.GameInfo.gameId;
                        Explorer.ExploradorObject.eventName = Server.EventNames.AskingForBifrost;
                        Explorer.ExploradorObject.message = "escritorio";
                        ServerEvents.OnSendExplorer.Invoke();
                        sfx.Play();
                    }
                    else if (Acao == Funcao.CallBifrostPraia)
                    {
                        timeLookedAt = 0;
                        slider.sliderValue = 0;
                        Explorer.ExploradorObject.gameId = WebSocketIOController.Instance.GameInfo.gameId;
                        Explorer.ExploradorObject.eventName = Server.EventNames.AskingForBifrost;
                        Explorer.ExploradorObject.message = "praia";
                        ServerEvents.OnSendExplorer.Invoke();
                        sfx.Play();
                    }
                    else if (Acao == Funcao.Animacao)
                    {
                        timeLookedAt = 0;
                        slider.sliderValue = 0;
                        GetComponent<Animator>().SetTrigger("Open");
                        this.enabled = false;
                        sfx.Play();
                    }
                    else if (Acao == Funcao.EndGame)
                    {
                        timeLookedAt = 0;
                        slider.sliderValue = 0;
                        GetComponent<BoxCollider>().enabled = false;

                        //endCanvas.SetActive(true);
                        //endAnim.SetTrigger("End");
                        //Invoke("Endgame", 20f);
                    }
                }
            }
        }
        else if(thisCanvas!=null && thisCanvas.activeSelf)
        {
            thisCanvas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explorer.ExploradorObject.gameId = WebSocketIOController.Instance.GameInfo.gameId;
            Explorer.ExploradorObject.eventName = Server.EventNames.DocumentDownloaded;
            Explorer.ExploradorObject.message = "1";
            ServerEvents.OnSendExplorer.Invoke();
        }
    }

    public void OnPointerEnter()
    {
        isLooking = true;
    }

    public void OnPointerExit()
    {
        isLooking = false;
        timeLookedAt = 0;
        slider.sliderValue = 0;
    }

    void Endgame()
    {
        Explorer.ExploradorObject.gameId = WebSocketIOController.Instance.GameInfo.gameId;
        Explorer.ExploradorObject.eventName = Server.EventNames.EndGame;
        Explorer.ExploradorObject.message = "end";
        ServerEvents.OnSendExplorer.Invoke();
        SceneManager.LoadScene("GDTStart");
    }

    void changeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToGo);
    }
}
