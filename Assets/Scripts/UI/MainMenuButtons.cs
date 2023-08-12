using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.GetComponent<Button>().onClick.AddListener(PlayButtonClick);
    }

    private void PlayButtonClick()
    {
        clickSound.Play();
        playButton.GetComponent<Button>().onClick.RemoveListener(PlayButtonClick);
        SceneLoader.ChangeScene("GameScene");
    }
}
