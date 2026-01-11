using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speak : PickupItem
{
    protected override void OnPickup(PlayerInteraction interactor)
    {
        if (itemName=="finish")
        {
            NextLevel.Intance.FinishLevel();
        }
    }
}
