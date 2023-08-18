using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Button backButton;

    [Header("Audio")]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle effectsToggle;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;

    private void Awake()
    {
        Subscribe();
        SetSettingsParameters();
    }

    private void Subscribe()
    {
        backButton.GetComponent<Button>().onClick.AddListener(OnBackButtonClick);

        musicToggle.onValueChanged.AddListener(OnMusicToggleChange);
        effectsToggle.onValueChanged.AddListener(OnEffectsToggleChange);
        musicSlider.onValueChanged.AddListener(OnMusicSliderChange);
        effectsSlider.onValueChanged.AddListener(OnEffectsSliderChange);
    }

    private void Unsubscribe()
    {
        backButton.GetComponent<Button>().onClick.RemoveListener(OnBackButtonClick);

        musicToggle.onValueChanged.RemoveListener(OnMusicToggleChange);
        effectsToggle.onValueChanged.RemoveListener(OnEffectsToggleChange);
        musicSlider.onValueChanged.RemoveListener(OnMusicSliderChange);
        effectsSlider.onValueChanged.RemoveListener(OnEffectsSliderChange);
    }

    private void SetSettingsParameters()
    {
        musicToggle.isOn = !AudioManager.instance.isMusicMuted;
        musicSlider.value = AudioManager.instance.musicVolume;

        effectsToggle.isOn = !AudioManager.instance.isEffectsMuted;
        effectsSlider.value = AudioManager.instance.effectsVolume;
    }

    private void OnBackButtonClick()
    {
        //Unsubscribe();
        AudioManager.instance.PlayEffect("Click");
        SceneLoader.LoadScene("MainMenu");
    }

    private void OnMusicToggleChange(bool isOn)
    {
        if (isOn)
            AudioManager.instance.UnmuteMusic();
        else
            AudioManager.instance.MuteMusic();
    }

    private void OnEffectsToggleChange(bool isOn)
    {
        if (isOn)
            AudioManager.instance.UnmuteEffects();
        else
            AudioManager.instance.MuteEffects();
    }

    private void OnMusicSliderChange(float value)
    {
        AudioManager.instance.SetMusicVolume(value);
    }

    private void OnEffectsSliderChange(float value)
    {
        AudioManager.instance.SetEffectsVolume(value);
    }
}
