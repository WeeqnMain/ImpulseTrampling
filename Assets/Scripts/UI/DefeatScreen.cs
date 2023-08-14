using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    [SerializeField] private TextMeshProUGUI highscoreBeatenText;
    [SerializeField] private TextMeshProUGUI currentHighscoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    private void Awake()
    {
        var button = restartButton.GetComponent<Button>();
        button.onClick.AddListener(RestartButtonClick);
    }

    public void Show(int score, bool isHighscoreBeaten)
    {
        currentScoreText.text = score.ToString();
        currentHighscoreText.text = $"HIGHSCORE: {PlayerPrefs.GetInt("highscore", 0)}";
        currentHighscoreText.enabled = !isHighscoreBeaten;
        highscoreBeatenText.enabled = isHighscoreBeaten;
        gameObject.SetActive(true);
        AudioManager.instance.PlayEffect("Defeat");
    }

    private void RestartButtonClick()
    {
        restartButton.onClick.RemoveListener(RestartButtonClick);
        AudioManager.instance.PlayEffect("Click");
        SceneLoader.RestartScene();
    }
}
