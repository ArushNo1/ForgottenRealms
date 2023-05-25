using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttack : MonoBehaviour
{
    public Stats my;
    public Collider2D rightCollider;
    Vector2 rightOffset;
    SpriteRenderer render;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rightOffset = rightCollider.offset;
        render = GetComponentInParent<SpriteRenderer>();
        my = GetComponent<Stats>();
    }

    public void RightAttack()
    {
        rightCollider.enabled = true;
        rightCollider.offset = rightOffset;
    }

    public void LeftAttack()
    {
        rightCollider.enabled = true;
        rightCollider.offset = new Vector2(rightOffset.x * -1, rightOffset.y);
    }  

    
     public void OnTriggerEnter2D(Collider2D other)
    {
        AnimatorStateInfo info = GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (other.GetComponent<Transform>() == target && info.IsName("Human_Attack_Right"))
        {
            Damageable player = other.GetComponent<Damageable>();
            player.Damage(my.damage);
        }
    }
}
