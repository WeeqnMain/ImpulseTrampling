using UnityEngine;
using UnityEngine.UI;

public class AboutSceneScript : MonoBehaviour
{
    [SerializeField] private Button backButton;

    private void Awake()
    {
        backButton.GetComponent<Button>().onClick.AddListener(OnBackButtonClick);
    }

    private void OnBackButtonClick()
    {
        AudioManager.instance.PlayEffect("Click");
        backButton.GetComponent<Button>().onClick.RemoveListener(OnBackButtonClick);
        SceneLoader.ChangeScene("MainMenu");
    }
}
