using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : MonoBehaviour
{
    [Header("Fondos")]
    public Image[] backgroundImages;

    [Header("Textos")]
    public TextMeshProUGUI[] texts;

    [Header("Iconos")]
    public Image[] icons;

    [Header("Timing")]
    public float backgroundFadeInDuration = 1f;
    public float textsStayDuration = 2f;
    public float backgroundFadeOutDuration = 1f;

    void Start()
    {
        // Estado inicial
        SetImagesAlpha(backgroundImages, 0f);
        SetTextsActive(false);
        SetImagesActive(icons, false);

        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        // 1️⃣ Fade IN de fondos
        yield return FadeImages(backgroundImages, 0f, 1f, backgroundFadeInDuration);

        // 2️⃣ Textos + iconos aparecen de golpe
        SetTextsActive(true);
        SetImagesActive(icons, true);

        // 3️⃣ Se quedan
        yield return new WaitForSeconds(textsStayDuration);

        // 4️⃣ Textos + iconos desaparecen
        SetTextsActive(false);
        SetImagesActive(icons, false);

        // 5️⃣ Fade OUT de fondos
        yield return FadeImages(backgroundImages, 1f, 0f, backgroundFadeOutDuration);

        gameObject.SetActive(false);
    }

    // ================== Helpers ==================

    void SetTextsActive(bool active)
    {
        foreach (var t in texts)
            t.gameObject.SetActive(active);
    }

    void SetImagesActive(Image[] images, bool active)
    {
        foreach (var img in images)
            img.gameObject.SetActive(active);
    }

    IEnumerator FadeImages(Image[] images, float from, float to, float duration)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float a = Mathf.Lerp(from, to, t);

            foreach (var img in images)
            {
                Color c = img.color;
                c.a = a;
                img.color = c;
            }
            yield return null;
        }
    }

    void SetImagesAlpha(Image[] images, float alpha)
    {
        foreach (var img in images)
        {
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }
    }
}
