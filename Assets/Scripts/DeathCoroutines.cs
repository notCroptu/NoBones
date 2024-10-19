using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DeathCoroutines : MonoBehaviour
{
    [SerializeField] private VideoPlayer vp;
    [SerializeField] private SoundScript ss;
    void Start()
    {
        vp.loopPointReached += OnVideoEnd;
    }

    public IEnumerator JokinhasNo()
    {
        vp.gameObject.SetActive(true);
        vp.Stop();
        vp.Play();
        yield return null;
    }
    public IEnumerator VouDesligarANet()
    {
        yield return null;
    }
    public IEnumerator VaisApanhar()
    {
        yield return null;
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);
    }
}
