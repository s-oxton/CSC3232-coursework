using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [SerializeField]
    private EnemyRaycast raycast;
    [SerializeField]
    private EnemyStatTracker statTracker;
    
    [SerializeField]
    private float attackGap;
    private float attackTimer;
    [SerializeField]
    private float attackGapResetTime;
    private float resetTimer;

    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private int damage;

    private bool playerHittable;
    

    // Update is called once per frame
    void Update()
    {

        playerHittable = raycast.GetPlayerAttackable();
        //if the player is hittable
        if (playerHittable)
        {
            //if there has been a large enough gap between attacks, or if the player has been outside of attack range for sufficiently long, attack
            if (attackTimer <= 0 || resetTimer <= 0)
            {
                statTracker.SetAttacking(true);
                attackTimer = attackGap;
            }
            //else reduce the attack timer
            else
            {
                attackTimer -= Time.deltaTime;
            }
            //since the player is hittable, set the gap timer to max
            resetTimer = attackGapResetTime;
        }
        //if the player is outside of hit range, reduce the gap timer
        else if (resetTimer >= 0)
        {
            resetTimer -= Time.deltaTime;
        }


    }

    //called during the frame of the monster's attack
    public void Attack()
    {
        //deal damage to player
        Collider2D player = Physics2D.OverlapCircle(attackPosition.position, attackRange, playerLayer);

        if (player != null)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(damage);
        }
    
    }

    //temp testing for seeing range of attack
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

}
