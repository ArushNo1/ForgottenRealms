using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D rightCollider;
    public Collider2D upCollider;

    Vector2 rightOffset;
    Vector2 upOffset;

    SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        rightOffset = rightCollider.offset;
        upOffset = upCollider.offset;
        render = GetComponentInParent<SpriteRenderer>();
    }

    private void Update() {
        
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

    public void UpAttack()
    {
        upCollider.enabled = true;
        upCollider.offset = upOffset;
    }

    public void DownAttack()
    {
        upCollider.enabled = true;
        upCollider.offset = new Vector2(upOffset.x, upOffset.y * -1);
    }

    
}
