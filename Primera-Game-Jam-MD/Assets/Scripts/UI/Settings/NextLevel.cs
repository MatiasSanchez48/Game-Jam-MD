using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public static NextLevel Intance;
    
    [SerializeField] public GameObject finishLevel;

    private void Awake()
    {
        Intance = this;
    }
    public void FinishLevel()
    {
        finishLevel.SetActive(true);
    }
    
}
