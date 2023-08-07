using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.GetComponent<Button>().onClick.AddListener(PlayButtonClick);
    }

    private void PlayButtonClick()
    {
        playButton.GetComponent<Button>().onClick.RemoveListener(PlayButtonClick);
        SceneLoader.ChangeScene("GameScene");
    }
}
