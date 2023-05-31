using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float width;
    public float height;
    public float damage;
    public float resistance;
    public float regeneration = 0f;
    public float attackCooldown = .5f;
    public float knockback = 1f;
    public float knockbackResistance = 0f;

    private float timeSinceAttack = 0f;

    public bool CanAttack{
        get{
            if(timeSinceAttack >= attackCooldown){
                timeSinceAttack = 0f;
                return true;
            }
            return false;
        }
    }

    public void Knockback(Vector2 directionTo){
        direction *= 1 - (knockbackResistance * .05f);
        GetComponent<Rigidbody2D>().AddForce(directionTo);
    }

    void Update()
    {
        if(timeSinceAttack < attackCooldown * 2){
            timeSinceAttack += Time.deltaTime;
        }
    }
}
