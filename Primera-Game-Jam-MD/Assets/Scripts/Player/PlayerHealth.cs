using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [Header("UI")]
    public GameObject gameOverUI;

    [Header("Player Health")]
    public float currentStamine;

    private PlayerStats stats;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        currentStamine = stats.stamina;
    }

    public void TakeDamage(float amount)
    {
        stats.stamina -= amount;
        stats.stamina = Mathf.Clamp(stats.stamina, 0, stats.maxStamina);

        //Debug.Log($"Player recibe {amount} de DAMAGE. Vida: {stats.stamina}");

        if (stats.stamina <= 0)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        stats.stamina += amount;
        stats.stamina = Mathf.Clamp(stats.stamina, 0, stats.maxStamina);
        //Debug.Log($"Player recibe {amount} de HEALTH. Vida: {stats.stamina}");
    }

    void Die()
    {
        Debug.Log("Player murio");
        gameOverUI.SetActive(true);
        GetComponent<PlayerController>().canMove = false;
        // m�s adelante: animaci�n, reload, checkpoint
    }
}
