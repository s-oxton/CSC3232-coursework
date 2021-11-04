using UnityEngine;

public class MushroomAnimator : MonoBehaviour
{

    [SerializeField]
    private DumbMovement dumbMovement;
    [SerializeField]
    private EnemyCombat enemyCombat;
    [SerializeField]
    private Animator animator;

    private int mushroomOrientation;

    private void Start()
    {
        mushroomOrientation = dumbMovement.GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCombat.GetCurrentHealth() <= 0)
        {
            animator.SetBool("IsDead", true);
        }
        else
        {
            if (enemyCombat.GetIsHit())
            {
                animator.SetBool("IsHit", true);
            }
            else
            {
                animator.SetBool("IsHit", false);
            }
        }

        animator.SetBool("Attacking", enemyCombat.GetAttacking());

        if (mushroomOrientation != dumbMovement.GetDirection())
        {
            mushroomOrientation = dumbMovement.GetDirection();
            SetXOrientation();
        }

        if (dumbMovement.GetIsMoving())
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }

    //flip the x orientation of the mushroom
    private void SetXOrientation()
    {
        Vector2 tempVector = transform.localScale;

        if (mushroomOrientation == 1)
        {
            tempVector.x = Mathf.Abs(tempVector.x);
        }
        else if (mushroomOrientation == -1)
        {
            tempVector.x = -Mathf.Abs(tempVector.x);
        }

        transform.localScale = tempVector;

    }

    //ends the current attack
    private void EndAttack()
    {
        enemyCombat.SetAttacking(false);
    }

    private void DealDamage()
    {
        enemyCombat.Attack();
    }

    //hittable is false when the combat animation is active
    private void UpdateHittable(int hittable)
    {
        //if hittable is 0 then the monster can't be hit
        if (hittable == 0)
        {
            enemyCombat.SetHittable(false);
        } else
        {
            enemyCombat.SetHittable(true);
        }
        
    }

    private void Attack()
    {
        enemyCombat.Attack();
    }

}
