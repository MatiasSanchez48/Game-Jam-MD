using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZone : MonoBehaviour
{
    public float healAmount = 25f;

    private void OnTriggerStay(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(healAmount * Time.deltaTime);
        }
    }
}
