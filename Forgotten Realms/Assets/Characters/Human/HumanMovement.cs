using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Threading;

public class HumanMovement : MonoBehaviour
{
    public Transform[] possibleEnemies;
    Transform target = null;
    public float sightDistance = 6f;
    public float releaseDistance = 10f;

    Vector2 movement;

    public float moveSpeed = 1f;
    public float nextWaypointDistance = .05f;
    Path path;
    int currentWaypoint = 0;
    Seeker seeker;

    HumanController controller;
    Rigidbody2D rb;

    public HumanAttack attack;

    void Start()
    {
        controller = GetComponent<HumanController>();
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        UpdateTarget();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    float multiplier = 1;

    void UpdatePath()
    {
        Vector2 targetPos = target.position;
        float targetX = target.position.x + target.GetComponent<Stats>().width * multiplier * .5f;
        float targetY = target.position.y + target.GetComponent<Stats>().height / 2;

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, targetPos, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        UpdateTarget();
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 amountToMove = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + amountToMove);

        float distanceToWP = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distanceToWP < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        UpdateDirection(amountToMove);
    }

    public void UpdateTarget()
    {
        float distance;
        if (target != null)
        {
            distance = Mathf.Sqrt(
                Mathf.Pow(target.position.x - transform.position.x, 2)
                    + Mathf.Pow(target.position.y - transform.position.y, 2)
            );
            if (distance > releaseDistance)
            {
                target = null;
            }
        }

        if (target == null)
        {
            float minDistance = sightDistance;

            foreach (Transform enemy in possibleEnemies)
            {
                distance = Mathf.Sqrt(
                    Mathf.Pow(transform.position.x - enemy.position.x, 2)
                        + Mathf.Pow(transform.position.y - enemy.position.y, 2)
                );
                //print("Distance to " + enemy + "Is " + distance);
                if (distance < minDistance)
                {
                    target = enemy;
                    //print("found closer enemy");
                    attack.target = enemy;
                    minDistance = distance;
                }
            }
        }
    }

    void UpdateDirection(Vector2 dir)
    {
        int direction = 0;

        if (dir.x > .05f)
        {
            controller.rend.flipX = false;
            direction = 1;
        }
        else if (dir.x < -.05f)
        {
            controller.rend.flipX = true;
            direction = 1;
        }
        else if (dir.y > .01f)
        {
            direction = 2;
        }
        else if (dir.y < .01f)
        {
            direction = 4;
        }

        print(direction);
        controller.anim.SetInteger("Direction", direction);
    }
}
