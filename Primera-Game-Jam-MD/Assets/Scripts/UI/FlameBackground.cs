using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class FlameBackground : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Image damageImage1;
    [SerializeField] public Image damageImage2;

    [Header("Effect")]
    [SerializeField] private float maxAlpha = 0.7f; // opacidad máxima
    private PlayerStats stats;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
    }
    void Update()
    {
        float staminaNormalized = stats.stamina / stats.maxStamina;
        float threshold = 0.35f;
        float alpha = 0f;

        if (staminaNormalized <= threshold)
        {
            // Normalizamos SOLO el tramo 0 → 35%
            float t = staminaNormalized / threshold;
            // 0 stamina = maxAlpha | 35% stamina = 0 alpha
            alpha = Mathf.Lerp(maxAlpha, 0f, t);
        }

        Color c = damageImage1.color;
        c.a = alpha;
        damageImage1.color = c;

        Color c2 = damageImage2.color;
        c2.a = alpha;
        damageImage2.color = c2;
    }
}
