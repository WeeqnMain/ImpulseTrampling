using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI scoreIncreaseText;

    public int score { get; private set; }

    public bool isHighscoreBeaten { get; private set; }

    private void IncreaseScore(int value = 1)
    {
        score += value;
        scoreLabel.text = score.ToString();
        StartCoroutine(DisplayIncreaseScore(value));
    }

    private IEnumerator DisplayIncreaseScore(int value)
    {
        scoreIncreaseText.text = $"+{value}";
        scoreIncreaseText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        scoreIncreaseText.gameObject.SetActive(false);
    }

    public void OnEnemyDestroyed(int pointsForDestroy)
    {
        IncreaseScore(pointsForDestroy);
    }

    public void SaveScore()
    {
        if (PlayerPrefs.HasKey("highscore") && score > PlayerPrefs.GetInt("highscore"))
        {
            isHighscoreBeaten = true;
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void HideScoreLabel()
    {
        scoreLabel.enabled = false;
    }
}
