using UnityEngine;
using System.Collections.Generic;

public class FlyingMonsterMovement : MonoBehaviour
{

    [SerializeField]
    private FlyingRaycast raycast;
    [SerializeField]
    private Transform playerTarget;
    [SerializeField]
    private EnemyStatTracker statTracker;
    [SerializeField]
    private FlyingMonsterCollision collision;
    [SerializeField]
    private Pathfinder pathfinder;

    [SerializeField]
    private CircleCollider2D circleCollider;
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float friction;
    [SerializeField]
    private float viewDistance;
    private bool playerAttackable;

    public float GetXVelocity()
    {
        return rb.velocity.x;
    }

    public bool GetPlayerAttackable()
    {
        return playerAttackable;
    }

    public float GetDistanceToPlayer()
    {
        float distance = 0;
        distance = Vector2.Distance(rb.position, GameObject.Find("Player").GetComponent<PlayerMovement>().GetPosition());
        return distance;
    }


    private void FixedUpdate()
    {
        Vector3 targetPosition;
        Vector3 targetDirection;
        bool playerVisible = false;

        //if the enemy is dead
        if (statTracker.GetCurrentHealth() <= 0)
        {
            //if monster is grounded, it can no longer move or interact
            if (collision.GetGrounded())
            {
                rb.isKinematic = true;
                circleCollider.enabled = false;
                Destroy(gameObject, 1f);
            }
            //else, make it fall.
            else
            {
                rb.gravityScale = 0.4f;
            }

        }
        else if (!statTracker.GetAttacking())
        {
            //if player is within view distance of the monster
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            targetDirection = (playerTarget.position - transform.position).normalized;

            //only bother raycasting if player is close enough
            if (distanceToPlayer <= raycast.GetViewDistance())
            {
                playerVisible = raycast.CheckRay(transform, targetDirection, "visibility");
                playerAttackable = raycast.CheckRay(transform, targetDirection, "attackable");
            }

            //if player is visible to the monster
            if (playerAttackable || statTracker.GetIsHit())
            {
                //stop moving so can attack
                rb.velocity = new Vector2(0, 0);
            }
            else if (playerVisible)
            {
                //fly towards player.
                MoveTowards(targetDirection);
            }
            else if (distanceToPlayer <= viewDistance)
            {
                //find the path
                pathfinder.FindPathToTarget(transform.position, playerTarget.position);
                //get the target position
                targetPosition = GetTargetPosition();
                //convert into direction
                targetDirection = (targetPosition - transform.position).normalized;
                //move towards the target
                MoveTowards(targetDirection);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    //converts the node path values into a vector position
    private Vector3 GetTargetPosition()
    {
        Vector3 target = new Vector3();
        List<Node> path = pathfinder.GetPath();
        int[] coords = new int[2];
        //if the path is long enough
        if (path.Count > 2)
        {
            coords = path[2].GetPosition();
            target = new Vector3(coords[0], coords[1], 0);
        }
        //get the coordinates of the next position to move to
        return target;
    }


    //if already moving in the right direction, then great
    //if moving in the wrong direction, need to slow down to 0 first!!

    private void MoveTowards(Vector3 direction)
    {

        //if moving in the wrong direction, apply "friction"
        //(if the current velocity and the direction to travel are in different directions)
        if (rb.velocity.magnitude > 0.5f && !CheckDirectionsMatch(direction, rb.velocity))
        {
            rb.AddForce(Mathf.Min(rb.velocity.magnitude, 1.5f) * friction * (-rb.velocity.normalized) * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        //else apply movement force as normal
        else
        {
            //calculate movement force.
            float movementForce = speed - Mathf.Abs(rb.velocity.magnitude);
            //add acceleration to force
            movementForce = Mathf.Pow(movementForce, acceleration);
            //add force to object.
            rb.AddForce(direction * movementForce * Time.fixedDeltaTime, ForceMode2D.Force);
        }
    }

    //checks if two vectors are in the same direction
    private bool CheckDirectionsMatch(Vector3 travelDirection, Vector3 targetDirection)
    {
        //if the vectors are in the same direction, return true
        if (Vector3.Dot(travelDirection, targetDirection) > 0)
        {
            return true;
        }

        return false;
    }

}
