using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private int _lives = 2;
    private int _score = 0;
    private TMP_Text _tmpScore;
    private Sprite _heartSprite;
    private Sprite _heartSprite2;
    
    void Start()
    {
        _heartSprite = Resources.Load<Sprite>("HeartUI");
        _heartSprite2 = Resources.Load<Sprite>("NoHeartUI");
        _tmpScore = canvas.transform.Find("ScoreText").GetComponent<TMP_Text>();
        UpdateHealthPanel();
        UpdateScoreText();
    }

    public void TakeDamage()
    {
        _lives--;
        UpdateHealthPanel();
        UpdateScoreText();
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
    }
    
    private void UpdateScoreText()
    {
        _tmpScore.text = $"Score: {_score.ToString()}";
    }
}
