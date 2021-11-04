using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [Header("References")]
    [Space(10)]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerCombat playerCombat;
    [SerializeField]
    private PlayerCollision playerCollision;

    private float xVelocity;

    //change the animations for the player
    private void Update()
    {

        animator.SetBool("IsHurt", playerCombat.GetIsHurt());

        xVelocity = playerMovement.GetXVelocity();

        animator.SetFloat("xSpeed", Mathf.Abs(xVelocity));

        animator.SetFloat("ySpeed", playerMovement.GetYVelocity());

        animator.SetBool("IsGrounded", playerCollision.GetGrounded());

        if (playerMovement.GetXInput() != 0)
        {
            AnimateRunSpeed(xVelocity, playerMovement.GetRunSpeed());
        }

        animator.SetBool("IsSkidding", playerMovement.GetIsFriction());

        animator.SetBool("IsCrouching", playerMovement.GetCrouching());

        PlayerFlip(xVelocity);

    }

    //trigger animation of player dying
    public void Dead()
    {
        animator.SetBool("IsDead", true);
    }

    //trigger animation of player dying
    public void SkidAttack()
    {
        animator.SetTrigger("SkidAttack");
    }

    //trigger animation of player dying
    public void RegularAttack()
    {
        animator.SetTrigger("RegularAttack");
    }

    //change run animation speed depending on the player's speed
    private void AnimateRunSpeed(float xVelocity, float runSpeed)
    {
        float animateSpeed;
        animateSpeed = Mathf.Max(0.5f, Mathf.Abs(xVelocity) / runSpeed * 1.2f);
        animator.SetFloat("RunAnimationSpeed", animateSpeed);
    }

    //change orientation of player depending on their direction
    private void PlayerFlip(float xVelocity)
    {
        if (Mathf.Abs(xVelocity) > 0.5f)
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

    }



}
