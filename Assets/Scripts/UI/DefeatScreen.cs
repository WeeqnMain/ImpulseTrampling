using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    [SerializeField] private TextMeshProUGUI highscoreBeatenText;
    [SerializeField] private TextMeshProUGUI currentHighscoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    private void Awake()
    {
        restartButton.GetComponent<Button>().onClick.AddListener(RestartButtonClick);
        menuButton.GetComponent<Button>().onClick.AddListener(MenuButtonClick);
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
        //restartButton.onClick.RemoveListener(RestartButtonClick);
        AudioManager.instance.PlayEffect("Click");
        SceneLoader.ReloadScene();
    }

    private void MenuButtonClick()
    {
        //menuButton.onClick.RemoveListener(MenuButtonClick);
        AudioManager.instance.PlayEffect("Click");
        SceneLoader.LoadScene("MainMenu");
    }
}
