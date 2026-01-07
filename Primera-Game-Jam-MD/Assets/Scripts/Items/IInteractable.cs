using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteraction interactor);
    string GetInteractionText(); // Texto tipo: "Recoger flor"
}
