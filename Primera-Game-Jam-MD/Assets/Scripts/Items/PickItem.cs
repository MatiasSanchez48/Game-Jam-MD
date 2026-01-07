using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Clase abstracta para los items.</para>
/// 
/// <para>Parametros:</para>
/// <para>name: itemName</para>
/// </summary>
public abstract class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] protected string itemName = "Item";
    [SerializeField] protected string interactionText = "Recoger item";
    [SerializeField] protected bool destroyOnInteract = true;

    /// <summary>
    /// Devuelve el texto de interaccion del item.
    /// </summary>
    /// <returns></returns>
    public string GetInteractionText()
    {
        return interactionText;
    }

    /// <summary>
    /// Funcion que se llama al interactuar con el item.
    /// </summary>
    /// <param name="interactor"></param>
    public void Interact(PlayerInteraction interactor)
    {
        OnPickup(interactor);
        Debug.Log($"Funcion General Recogiendo {itemName}");

        if (destroyOnInteract)
            Destroy(gameObject);
    }

    /// <summary>
    ///  /// Se llama al Interectuar<Interact> de la funcion asi los items 
    /// pueden ser recogidos con esto se puede crear un sistema de pickeos.
    /// </summary>
    /// <param name="interactor"></param>
    protected abstract void OnPickup(PlayerInteraction interactor);
}
