using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeUI : MonoBehaviour
{
    public float intensity = 5f;   // cuánto tiembla
    public float speed = 20f;      // qué tan rápido

    private RectTransform rect;
    private Vector2 startPos;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
    }

    void Update()
    {
        float x = Mathf.PerlinNoise(Time.time * speed, 0f) - 0.5f;
        float y = Mathf.PerlinNoise(0f, Time.time * speed) - 0.5f;

        rect.anchoredPosition = startPos + new Vector2(x, y) * intensity;
    }

    public void ResetShake()
    {
        rect.anchoredPosition = startPos;
    }
}
