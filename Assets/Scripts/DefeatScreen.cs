using UnityEngine;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        var button = restartButton.GetComponent<Button>();
        button.onClick.AddListener(RestartButtonClick);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void RestartButtonClick()
    {
        restartButton.onClick.RemoveListener(RestartButtonClick);
        SceneLoader.RestartScene();
    }
}
