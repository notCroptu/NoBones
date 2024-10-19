using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class DeathCoroutines : MonoBehaviour
{
    [SerializeField] private VideoPlayer vp;
    [SerializeField] private SoundScript ss;


    [SerializeField] private GameObject player;
    private Scene currentScene;
    [SerializeField] private Sounds sounds;
    [SerializeField] private SoundScript audioPlayer;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        audioPlayer = GetComponent<SoundScript>();
        sounds = GetComponent<Sounds>();
    }

    public IEnumerator GameOver()
    {
        audioPlayer.PlayAudio(sounds.Die);
        WaitForSeconds ws = new WaitForSeconds(0.5f);

        for (int i = 0; i <= 3; i++ )
        {
            yield return ws;
        }

        vp.gameObject.SetActive(true);
        vp.Stop();
        vp.Play();
        while (vp.isPlaying)
        {
            yield return null;
        }
        vp.gameObject.SetActive(false);
        SceneManager.LoadScene(currentScene.name);
    }

    public IEnumerator VouDesligarANet()
    {
        audioPlayer.PlayAudio(sounds.Net);
        yield return null;
    }
    public IEnumerator VaisApanhar()
    {
        audioPlayer.PlayAudio(sounds.Vassoura);
        yield return null;
    }
}
