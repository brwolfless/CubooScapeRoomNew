using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour
{
    public FadeScreen FadeScreen;
    public int sceneIndex; // Índice da cena a ser carregada
    public AudioSource SoundFX;
    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto colidiu com o objeto que possui este script
        if (other.CompareTag("Player"))
        {
             SoundFX.Play();
            StartCoroutine(GoToSceneRoutine(sceneIndex));
            
        }
    }
    IEnumerator GoToSceneRoutine(int sceneIndex) 
    {
        FadeScreen.FadeOut();
        yield return new WaitForSeconds(FadeScreen.fadeDuration);
        SceneManager.LoadScene(sceneIndex);
    }
    
}