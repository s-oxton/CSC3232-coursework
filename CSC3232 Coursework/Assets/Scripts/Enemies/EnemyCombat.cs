using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [SerializeField]
    private EnemyRaycast raycast;

    [SerializeField]
    private int maxHealth;
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

    private int currentHealth;
    private bool playerHittable;
    private bool hittable;
    private bool isHit;
    private bool isAttacking;

    #region getters/setters
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void EndHit()
    {
        isHit = false;
    }

    public bool GetIsHit()
    {
        return isHit;
    }

    public void SetHittable(bool tempHittable)
    {
        hittable = tempHittable;
    }

    public bool GetAttacking()
    {
        return isAttacking;
    }

    public void SetAttacking(bool attacking)
    {
        isAttacking = attacking;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        hittable = true;
    }

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
                isAttacking = true;
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

    //when the monster takes damage
    public void TakeDamage(int damageAmount)
    {
        //if the monster is hittable (not doing an uncancelable animation)
        if (hittable)
        {
            //stop the attack animation
            isHit = true;
            isAttacking = false;
        }
        //always reduce the health of the monster
        currentHealth -= damageAmount;

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
