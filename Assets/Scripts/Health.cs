using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthMax;
    [Header("Debug")]
    [SerializeField] private int healthCurrent;

    private void Start()
    {
        healthCurrent = healthMax;
    }

    public void TakeDamage(int value){
        healthCurrent = Math.Clamp(healthCurrent - value, 0, healthMax);
        if(healthCurrent == 0 ){
            Die();
        }
    }

    public void Heal(int value){
        healthCurrent = Math.Clamp(healthCurrent + value, 0, healthMax);
    }

    public void Die(){

    }

}
