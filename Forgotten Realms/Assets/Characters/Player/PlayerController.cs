using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    //handles the movement of the player
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer render;
    public SwordAttack swordAttack;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        int moveDir = 0;
        //if the movement is not zero move
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool couldMove = TryToMove(movementInput);
                if (!couldMove && movementInput.x != 0)
                {
                    couldMove = TryToMove(new Vector2(movementInput.x, 0));
                }
                if (!couldMove && movementInput.y != 0)
                {
                    couldMove = TryToMove(new Vector2(0, movementInput.y));
                }

                if (!couldMove)
                {
                    moveDir = 0;
                }
                else
                {
                    moveDir = getDirectionNum();
                }
                animator.SetInteger("MoveDirection", moveDir);
            }
            else
            {
                animator.SetInteger("MoveDirection", 0);
            }
        }
    }

    private int getDirectionNum()
    {
        int direction = 0;
        if (movementInput.x > 0)
        {
            direction = 1;
            render.flipX = false;
        }
        else if (movementInput.x < 0)
        {
            direction = 1;
            render.flipX = true;
        }
        else if (movementInput.y > 0)
        {
            direction = 2;
        }
        else if (movementInput.y < 0)
        {
            direction = 4;
        }
        return direction;
    }

    private bool TryToMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        return false;
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (
            (
                info.IsName("Player_Attack_Right")
                || info.IsName("Player_Attack_Up")
                || info.IsName("Player_Attack_Down")
            )
        )
        {
            Damageable damage = other.GetComponent<Damageable>();
            print("attempted to attack");
            if(damage != null)
                damage.Damage(10f);
        }
    }

    void OnFire()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (
            !(
                info.IsName("Player_Attack_Right")
                || info.IsName("Player_Attack_Up")
                || info.IsName("Player_Attack_Down")
            )
        )
        {
            animator.SetTrigger("SwordAttack");
        }
    }

    public void SwordAttack(int num)
    {
        canMove = false;
        if (num == 1)
        {
            if (render.flipX)
            {
                swordAttack.LeftAttack();
            }
            else
            {
                swordAttack.RightAttack();
            }
        }
        else if (num == 2)
        {
            swordAttack.UpAttack();
        }
        else if (num == 4)
        {
            swordAttack.DownAttack();
        }
    }

    public void EndSwordAttack(int number)
    {
        canMove = true;
        swordAttack.rightCollider.enabled = false;
        swordAttack.upCollider.enabled = false;
    }
}
