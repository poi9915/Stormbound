using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private TMP_Dropdown graphicsDropdown;
    [SerializeField] private TMP_Dropdown controlsDropdown;

    void Start()
    {
        // Load giá trị cũ (PlayerPrefs)
        soundSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
        graphicsDropdown.value = PlayerPrefs.GetInt("graphics", 2);
        controlsDropdown.value = PlayerPrefs.GetInt("controls", 0);

        // Gán sự kiện
        soundSlider.onValueChanged.AddListener(SetVolume);
        graphicsDropdown.onValueChanged.AddListener(SetGraphics);
        controlsDropdown.onValueChanged.AddListener(SetControls);
    }

    void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }

    void SetGraphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("graphics", index);
    }

    void SetControls(int index)
    {
        PlayerPrefs.SetInt("controls", index);
        // Sau có thể map input tùy index
    }
}
