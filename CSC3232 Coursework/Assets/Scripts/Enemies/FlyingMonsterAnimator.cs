
using UnityEngine;

public class FlyingMonsterAnimator : MonoBehaviour
{

    [SerializeField]
    private FlyingMonsterMovement movement;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private EnemyStatTracker statTracker;
    [SerializeField]
    private FlyingMonsterCollision monsterCollision;
    [SerializeField]
    private FlyingMonsterCombat monsterCombat;

    private void Update()
    {
        //if monster dead, set the animation to be dead
        if (statTracker.GetCurrentHealth() <= 0)
        {
            animator.SetBool("IsDead", true);
        }
        else
        {
            if (statTracker.GetIsHit())
            {
                animator.SetBool("IsHit", true);
            }
            else
            {
                animator.SetBool("IsHit", false);
            }
        }

        animator.SetBool("Attacking", statTracker.GetAttacking());

        //for the splat animation when the monster hits the ground
        if (monsterCollision.GetGrounded() && statTracker.GetCurrentHealth() <= 0)
        {
            animator.SetBool("IsSplat", true);
        }

        //update orientation of sprite if not dead
        if (statTracker.GetCurrentHealth() > 0 && Mathf.Abs(movement.GetXVelocity()) > 0.5f)
        {
            FlipSprite(movement.GetXVelocity());
        }
    }

    private void FlipSprite(float xVelocity)
    {
        Vector2 tempVector = transform.localScale;

        if (xVelocity > 0f)
        {
            tempVector.x = Mathf.Abs(tempVector.x);
        }
        else if (xVelocity < 0f)
        {
            tempVector.x = -Mathf.Abs(tempVector.x);
        }
        transform.localScale = tempVector;
    }

    public void EndAttack()
    {
        statTracker.SetAttacking(false);
    }

}
