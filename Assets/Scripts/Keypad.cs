using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public string code;
    public string correctCode = "1234";
    public AudioSource audioSource;
    public AudioClip correct;
    public AudioClip incorrect;
    public AudioClip click;
    public Animator doorAnim;
    public GameObject tpDoor;
    public BoxCollider[] teclas;

    public void AddKey(string key)
    {
        code += key;

        if(code.Length == 4)
        {
            if (code == correctCode)
            {
                Debug.Log("Code is correct!");
                audioSource.clip = correct;
                audioSource.Play();
                doorAnim.SetTrigger("OpenDoor");
                tpDoor.SetActive(true);
                for(int i = 0; i < teclas.Length; i++)
                {
                    teclas[i].enabled = false;
                }
            }
            else
            {
                Debug.Log("Code is incorrect!");
                audioSource.clip = incorrect;
                audioSource.Play();
            }

            code = "";
        }
        else
        {
            audioSource.clip = click;
            audioSource.Play();
        }
    }
}
