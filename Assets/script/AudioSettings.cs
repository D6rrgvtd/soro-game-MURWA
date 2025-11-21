using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider seSlider;

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);
    }

    void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", value);
    }

    void SetBGMVolume(float value)
    {
        audioMixer.SetFloat("BGMVolume", value);
    }

    void SetSEVolume(float value)
    {
        audioMixer.SetFloat("SEVolume", value);
    }
}
