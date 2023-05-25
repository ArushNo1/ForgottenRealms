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
    int moveDir = 0;
    public Transform playerPosition;

    public int moveDirection
    {
        get => moveDir;
        set => moveDir = value;
    }

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
    }

    private void FixedUpdate() { }

    void Update()
    {
        animator.SetInteger("Direction", moveDir);
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
