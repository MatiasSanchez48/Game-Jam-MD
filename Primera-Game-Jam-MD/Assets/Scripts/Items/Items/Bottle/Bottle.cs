using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : PickupItem
{
    private PlayerInventory inventory;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        inventory = player.GetComponent<PlayerInventory>();
    }
    protected override void OnPickup(PlayerInteraction interactor)
    {
        if (inventory != null && inventory.AddBottle())
        {
            UIInventory ui = FindObjectOfType<UIInventory>();
            ui.Refresh();
        }
    }
}
