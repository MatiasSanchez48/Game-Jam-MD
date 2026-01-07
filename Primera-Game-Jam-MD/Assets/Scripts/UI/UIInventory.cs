using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public PlayerInventory inventory;
    public GameObject bottleSlotPrefab;
    public Transform slotParent;

    public void Refresh()
    {
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        for (int i = 0; i < inventory.bottleCount; i++)
        {
            Instantiate(bottleSlotPrefab, slotParent);
        }
    }
}
