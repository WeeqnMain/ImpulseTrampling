using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button aboutButton;

    private void Awake()
    {
        Subscribe();
    }
    
    private void Subscribe()
    {
        playButton.GetComponent<Button>().onClick.AddListener(OnPlayButtonClick);
        settingsButton.GetComponent<Button>().onClick.AddListener(OnSettingsButtonClick);
        aboutButton.GetComponent<Button>().onClick.AddListener(OnAboutButtonClick);
    }

    private void Unsubscribe()
    {
        playButton.GetComponent<Button>().onClick.RemoveListener(OnPlayButtonClick);
        settingsButton.GetComponent<Button>().onClick.RemoveListener(OnSettingsButtonClick);
        aboutButton.GetComponent<Button>().onClick.RemoveListener(OnAboutButtonClick);
    }

    private void OnPlayButtonClick()
    {
        //Unsubscribe();
        AudioManager.instance.PlayEffect("Click");
        SceneLoader.LoadScene("GameScene");
    }

    private void OnSettingsButtonClick()
    {
        //Unsubscribe();
        AudioManager.instance.PlayEffect("Click");
        SceneLoader.LoadScene("Settings");
    }

    private void OnAboutButtonClick()
    {
        //Unsubscribe();
        AudioManager.instance.PlayEffect("Click");
        SceneLoader.LoadScene("About");
    }
}
