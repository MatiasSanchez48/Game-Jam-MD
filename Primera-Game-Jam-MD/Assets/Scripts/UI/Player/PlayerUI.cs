using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;

    private PlayerStats stats;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();

        healthBar.maxValue = stats.maxStamina;
    }

    void Update()
    {
        healthBar.value = stats.stamina;
    }
}
