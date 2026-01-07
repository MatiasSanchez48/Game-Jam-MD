using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [Header("Combat")]
    public int strength = 10;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina = 100f;
    public float staminaRegen = 15f;

    [Header("Vitals")]
    public float maxHealth = 100f;
    public float health = 100f;

    void Awake()
    {
        stamina = maxStamina;
        health = maxHealth;
    }

    public void AddStrength(int amount)
    {
        strength += amount;
    }

    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
    }

    public void ConsumeStamina(float amount)
    {
        stamina -= amount;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }

    public void RegenerateStamina()
    {
        stamina += staminaRegen * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }
}

