using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    , IPointerUpHandler
{
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = Vector3.one * 1.1f;
    public Vector3 pressedScale = Vector3.one * 0.95f;
    public float speed = 10f;

    private Vector3 targetScale;
    private bool isPointerInside;

    void Start()
    {
        targetScale = normalScale;
    }

    void Update()
    {
        transform.localScale =
            Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;
        targetScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;
        targetScale = normalScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = pressedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Si soltó el mouse fuera del botón, volvemos a normal
        targetScale = isPointerInside ? hoverScale : normalScale;
    }
}


