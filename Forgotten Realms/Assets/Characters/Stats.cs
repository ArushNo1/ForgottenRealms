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

    void Update()
    {
        if(timeSinceAttack < attackCooldown * 2){
            timeSinceAttack += Time.deltaTime;
        }
    }
}
