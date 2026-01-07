using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BottleSlot : MonoBehaviour
{
    public float healAmount = 35f;

    private PlayerInventory inventory;
    private PlayerHealth health;
    private UIInventory ui;

    void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
        health = inventory.GetComponent<PlayerHealth>();
        ui = FindObjectOfType<UIInventory>();

        GetComponent<Button>().onClick.AddListener(UseBottle);
    }

    void UseBottle()
    {
        if (inventory.UseBottle())
        {
            health.Heal(healAmount);
            ui.Refresh();
        }
    }
}
