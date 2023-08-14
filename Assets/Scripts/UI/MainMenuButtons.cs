using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button aboutButton;

    private void Awake()
    {
        playButton.GetComponent<Button>().onClick.AddListener(OnPlayButtonClick);
        settingsButton.GetComponent<Button>().onClick.AddListener(OnSettingsButtonClick);
        aboutButton.GetComponent<Button>().onClick.AddListener(OnAboutButtonClick);
    }

    private void OnPlayButtonClick()
    {
        AudioManager.instance.PlayEffect("Click");
        playButton.GetComponent<Button>().onClick.RemoveListener(OnPlayButtonClick);
        SceneLoader.ChangeScene("GameScene");
    }

    private void OnSettingsButtonClick()
    {
        AudioManager.instance.PlayEffect("Click");
        settingsButton.GetComponent<Button>().onClick.RemoveListener(OnSettingsButtonClick);
        SceneLoader.ChangeScene("Settings");
    }

    private void OnAboutButtonClick()
    {
        AudioManager.instance.PlayEffect("Click");
        aboutButton.GetComponent<Button>().onClick.RemoveListener(OnAboutButtonClick);
        SceneLoader.ChangeScene("About");
    }
}
