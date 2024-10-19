using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        foreach (Transform child in GameObject.Find("Bones").transform)
        {
            Vector3 loc = child.transform.position;
            Destroy(child.gameObject);
            GameObject boneOb = Instantiate(boneObj, loc, Quaternion.identity);
            boneOb.transform.SetParent(null);
        }
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
        UpdateHealthPanel();
        UpdateScoreText();
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
}
