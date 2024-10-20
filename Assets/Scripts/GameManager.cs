using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private int _lives = 3;
    private float _maxScore = 200f;
    private int _score = 0;
    private TMP_Text _tmpScore;
    private Sprite _heartSprite;
    private Sprite _heartSprite2;
    [SerializeField] private GameObject boneObj;
    private RectTransform _scorePercBar;
    private float _maxJumpForceTime = 1.0f;
    private RectTransform _leftJumpBar;
    private RectTransform _rightJumpBar;
    

    [SerializeField] private GameObject videoPlayer;
    [SerializeField] private GameObject videoCanvas;


    [SerializeField] private GameObject player;
    private Scene currentScene;
    [SerializeField] private Sounds sounds;
    private SoundScript audioPlayer;
    
    void Start()
    {
        _heartSprite = Resources.Load<Sprite>("HeartUI");
        _heartSprite2 = Resources.Load<Sprite>("NoHeartUI");
        _tmpScore = canvas.transform.Find("ScoreText").GetComponent<TMP_Text>();
        _scorePercBar = canvas.transform.Find("ScorePanel").Find("Bar").GetComponent<RectTransform>();
        _scorePercBar.localScale = new Vector3(1, 1, 1);
        _leftJumpBar = canvas.transform.Find("LeftJumpForce").Find("bar").GetComponent<RectTransform>();
        _rightJumpBar = canvas.transform.Find("RightJumpForce").Find("bar").GetComponent<RectTransform>();
        UpdateHealthPanel();
        UpdateScoreText();
        UpdateScoreBar();
        UpdateLeftJumpBar();
        UpdateRightJumpBar();


        if (boneObj != null)
        {
            int counter = 0;
            foreach (Transform child in GameObject.Find("Bones").transform)
            {
                Vector3 loc = child.transform.position;
                Destroy(child.gameObject);
                GameObject boneOb = Instantiate(boneObj, loc, Quaternion.identity);
                boneOb.name = $"BoneOb{counter}";
                boneOb.transform.SetParent(null);

                counter ++;
            }
        }
        currentScene = SceneManager.GetActiveScene();

        audioPlayer = GetComponent<SoundScript>();
        sounds = GetComponent<Sounds>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //    StartCoroutine(GameOver());
    }
    public void UpdateLeftJumpBar(float force = 0f)
    {
        float perc = (force / _maxJumpForceTime);
        _leftJumpBar.localScale = new Vector3(1, perc, 1);
        _leftJumpBar.transform.parent.gameObject.SetActive(force > 0.0f);
    }
    public void UpdateRightJumpBar(float force = 0f)
    {
        float perc = (force / _maxJumpForceTime);
        _rightJumpBar.localScale = new Vector3(1, perc, 1);
        _rightJumpBar.transform.parent.gameObject.SetActive(force > 0.0f);
    }
    
    public void TakeDamage()
    {
        _lives--;

        StartCoroutine(Blink());
        audioPlayer.PlayAudio(sounds.Die);
        
        if (_lives == 1) StartCoroutine(VaisApanhar());
        else if (_lives == 2) StartCoroutine(DesligarANet());

        UpdateHealthPanel();
        UpdateScoreText();

        if (_lives == 0) StartCoroutine(GameOver());
    }
    private IEnumerator Blink()
    {
        WaitForSeconds ws = new WaitForSeconds(0.2f);
        SpriteRenderer sp = player.GetComponent<SpriteRenderer>();

        for (int i = 0; i <= 6; i++ )
        {
            sp.enabled = !sp.enabled;
            yield return ws;
        }
        if (!sp.enabled) sp.enabled = true;
    }

    private void UpdateScoreBar()
    {
        _scorePercBar.localScale = new Vector3((_score / _maxScore), 1, 1);
    }
    
    private void UpdateHealthPanel()
    {
        int count = 0;
        foreach (Transform child in canvas.transform.Find("HealthPanel").transform)
        {
            count++;
            if (count > _lives)
            {
                child.GetComponent<Image>().sprite = _heartSprite2;
            }
            else
            {
                child.GetComponent<Image>().sprite = _heartSprite;
            }
        }
    }

    public void AddScore() => AddScore(1);

    public void AddScore(int score)
    {
        _score += score;
        UpdateScoreText();
        UpdateScoreBar();;
    }
    
    private void UpdateScoreText()
    {
        _tmpScore.text = $"Score: {_score.ToString()}";
    }

    private IEnumerator GameOver()
    {
        Debug.Log("CogWheel");

        videoPlayer.SetActive(true);
        videoCanvas.SetActive(true);

        VideoPlayer vp = videoPlayer.GetComponent<VideoPlayer>();
        RawImage ri = videoCanvas.GetComponentInChildren<RawImage>();
        ri.color = new Color();

        vp.Stop();
        vp.Prepare();

        yield return new WaitUntil(() => vp.isPrepared);

        vp.Play();

        Debug.Log($"vp is playing, {vp.isPlaying}");

        float timer = 0f;

        while (vp.isPlaying)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0F, 1f, timer / 0.7f);
            ri.color = new Color(ri.color.r, ri.color.b, ri.color.a, newAlpha);
            yield return null;
        }

        videoPlayer.SetActive(false);
        videoCanvas.SetActive(false);
        SceneManager.LoadScene(currentScene.name);
    }
    public IEnumerator DesligarANet()
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
