using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{

    [SerializeField]
    private Slider _musicSlider, _masterSlider, _effectSlider;
    [SerializeField]
    private Toggle _low, _med, _high;

    [SerializeField]
    private AudioMixer _mixer;

    private void Start()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        _effectSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

        switch (PlayerPrefs.GetInt("QualityLevel", 1))
        {
            case 0:
                _low.isOn = true;
                QualitySettings.SetQualityLevel(0, true);
                break;
            case 1:
                _med.isOn = true;
                QualitySettings.SetQualityLevel(1, true);
                break;
            case 2:
                _high.isOn = true;
                QualitySettings.SetQualityLevel(2, true);
                break;
            default:
                _med.isOn = true;
                QualitySettings.SetQualityLevel(1, true);
                break;
        }
    }
    public void MasterVolume()
    {
        float sliderValue = _masterSlider.value;
        _mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void MusicVolume()
    {
        float sliderValue = _musicSlider.value;
        _mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void EffectsVolume()
    {
        float sliderValue = _effectSlider.value;
        _mixer.SetFloat("EffectVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("EffectVolume", sliderValue);
    }

    public void ChangeQuality(int change)
    {
        QualitySettings.SetQualityLevel(change, true);
        PlayerPrefs.SetInt("QualityLevel", change);
        Debug.Log($"Quality Level set to {QualitySettings.GetQualityLevel()}");
    }

    public void Back()
    {
        SceneManager.UnloadSceneAsync("Settings");
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
