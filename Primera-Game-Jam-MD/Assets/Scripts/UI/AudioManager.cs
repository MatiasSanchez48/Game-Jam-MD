using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Range(0f, 1f)]
    public float masterVolume = 1f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Cargar volumen guardado
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        ApplyVolume();
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
        ApplyVolume();
    }

    void ApplyVolume()
    {
        AudioListener.volume = masterVolume;
    }
}
