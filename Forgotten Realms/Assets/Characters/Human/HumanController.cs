using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanController : MonoBehaviour
{
    //handles the movement of the player
    Animator animator;
    SpriteRenderer render;
    int playerLayer;
    public Transform playerPosition;
    HumanAttack attack;


    public Animator anim
    {
        get { return animator; }
    }

    public SpriteRenderer rend
    {
        get { return render; }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        playerLayer = playerPosition.GetComponent<SpriteRenderer>().sortingOrder;
        attack = GetComponentInChildren<HumanAttack>();
    }

    void Update()
    {
        animator.SetTrigger("Attack");
    }

    public void SwordAttack(int num)
    {
        animator.SetTrigger("SwordAttack");
        if (render.flipX)
        {
            //swordAttack.LeftAttack();
        }
        else
        {
            //swordAttack.RightAttack();
        }
    }

    public void EndSwordAttack(int number)
    {
        //swordAttack.rightCollider.enabled = false;
        //swordAttack.upCollider.enabled = false;
    }
}
