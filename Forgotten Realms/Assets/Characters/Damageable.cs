using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    public float health = 100f;
    public float resistance = 1f;
    private float damageCoolDown = .5f;
    private float damageTime = 0f;
    public HealthBarController healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    public void Damage(float amount)
    {
        if (damageTime >= damageCoolDown)
        {
            amount *= Random.Range(.8f, 1.2f);
            float resistanceBlock = resistance * .04f;
            amount *= (1 - resistanceBlock);
            if (amount < 0)
                amount = 0;
            health -= amount;
            damageTime = 0f;
            healthBar.ChangeHealth(amount);
        }
        
    }

    private void FixedUpdate()
    {
        if (damageTime < damageCoolDown)
        {
            damageTime += Time.deltaTime;
        }
    }
}
