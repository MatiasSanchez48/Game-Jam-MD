using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int bottleCount;
    public int maxBottles = 3;

    public bool AddBottle()
    {
        if (bottleCount >= maxBottles)
            return false;

        bottleCount++;
        Debug.Log($"Bottle count: {bottleCount}");
        return true;
    }

    public bool UseBottle()
    {
        Debug.Log($"Bottle count: {bottleCount}");
        if (bottleCount <= 0)
            return false;

        bottleCount--;
        return true;
    }
}
