using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject isMutted;

    void Start()
    {
        if (!slider)
            slider = GetComponent<Slider>();

        slider.onValueChanged.RemoveAllListeners();

        if (AudioManager.Instance == null)
            return;

        slider.value = AudioManager.Instance.masterVolume;
        slider.onValueChanged.AddListener(OnSliderChanged);
    }
    void Update()
    {
        isMutted.SetActive(AudioManager.Instance.masterVolume == 0);
    }
    public void MaxOrMinVolume()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMasterVolume(slider.value == 0 ? 1 : 0);

        


        slider.value = AudioManager.Instance.masterVolume;
    }

    void OnSliderChanged(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }

    
}
