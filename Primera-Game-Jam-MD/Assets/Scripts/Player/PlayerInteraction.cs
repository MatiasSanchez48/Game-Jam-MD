using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;

    [Header("Interaction")]
    public float interactRadius = 3f;
    public LayerMask interactableMask;

    [Header("UI")]
    public GameObject backgroundUI;
    public GameObject textUI;
    public bool isInteracting = true;

    private IInteractable currentInteractable;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        CheckInteraction();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }
    }

    void CheckInteraction()
    {
        currentInteractable = null;

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            interactRadius,
            interactableMask
        );

        float closestDistance = Mathf.Infinity;

        foreach (var col in hits)
        {
            if (!col.TryGetComponent<IInteractable>(out var interactable))
                continue;

            float distance = Vector3.Distance(transform.position, col.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentInteractable = interactable;
            }
        }

        if (currentInteractable != null && isInteracting)
        {
            backgroundUI.SetActive(true);
            textUI.SetActive(true);
            textUI.GetComponent<TextMeshProUGUI>().text = currentInteractable.GetInteractionText();
        }
        else
        {
            backgroundUI.SetActive(false);
            textUI.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
